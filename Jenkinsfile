pipeline {
    agent {
        kubernetes {
            yaml """
apiVersion: v1
kind: Pod
metadata:
  labels:
    app: jenkins-agent
spec:
  containers:
  - name: dotnet
    image: mcr.microsoft.com/dotnet/sdk:7.0
    command:
    - cat
    tty: true
    volumeMounts:
    - mountPath: /workspace
      name: workspace-volume
  volumes:
  - name: workspace-volume
    emptyDir: {}
"""
        }
    }

    environment {
        SONAR_HOST = credentials('sonar-host-url')
        SONAR_TOKEN = credentials('sonar-token')
        DOTNET_CLI_HOME = "/tmp/dotnet_cli_home"
        BRANCH_NAME = "${env.BRANCH_NAME}"
        APP_PORT = calculatePortForBranch("${env.BRANCH_NAME}")
        K8S_NAMESPACE = "default"
        DEPLOYMENT_NAME = "dotnet-app-${env.BRANCH_NAME.replaceAll('/', '-')}"
    }
    
    stages {
        stage('Checkout') {
            steps {
                checkout scm
            }
        }

        stage('Build and Test') {
            steps {
                container('dotnet') {
                    sh '''
                        dotnet restore
                        dotnet build --configuration Release --no-restore
                        dotnet test --no-restore --verbosity normal
                    '''
                }
            }
        }

        stage('SonarQube Analysis') {
            steps {
                container('dotnet') {
                    withSonarQubeEnv('SonarQube') {
                        sh '''
                            dotnet tool install --global dotnet-sonarscanner || true
                            export PATH="$PATH:$HOME/.dotnet/tools"
                            dotnet sonarscanner begin /k:"${JOB_NAME}" /d:sonar.host.url="${SONAR_HOST}" /d:sonar.login="${SONAR_TOKEN}"
                            dotnet build --configuration Release
                            dotnet sonarscanner end /d:sonar.login="${SONAR_TOKEN}"
                        '''
                    }
                }
            }
        }

        stage('Publish') {
            steps {
                container('dotnet') {
                    sh 'dotnet publish -c Release -o ./publish'
                }
            }
        }

        stage('Deploy to Kubernetes') {
            when {
                anyOf {
                    branch 'main'
                    branch 'develop'
                    branch pattern: 'feature/*', comparator: 'GLOB'
                }
            }
            steps {
                script {
                    sh """
                    kubectl apply -n ${K8S_NAMESPACE} -f - <<EOF
                    apiVersion: apps/v1
                    kind: Deployment
                    metadata:
                      name: ${DEPLOYMENT_NAME}
                      namespace: ${K8S_NAMESPACE}
                    spec:
                      replicas: 1
                      selector:
                        matchLabels:
                          app: ${DEPLOYMENT_NAME}
                      template:
                        metadata:
                          labels:
                            app: ${DEPLOYMENT_NAME}
                        spec:
                          containers:
                          - name: dotnet-app
                            image: mcr.microsoft.com/dotnet/aspnet:7.0
                            ports:
                            - containerPort: 80
                            volumeMounts:
                            - name: app-volume
                              mountPath: /app
                          volumes:
                          - name: app-volume
                            hostPath:
                              path: ${WORKSPACE}/publish
                    EOF
                    """
                }
            }
        }
    }

    post {
        always {
            cleanWs(cleanWhenNotBuilt: false, deleteDirs: true, disableDeferredWipeout: true, notFailBuild: true)
        }
        success {
            echo "Deployment successful! Application is running on Kubernetes."
        }
        failure {
            echo "Build or deployment failed!"
        }
    }
}

// Función para calcular el puerto basado en la rama
def calculatePortForBranch(branchName) {
    if (branchName == 'main') {
        return 8080
    } else if (branchName == 'develop') {
        return 8081
    } else if (branchName.startsWith('feature/')) {
        def featureName = branchName.substring('feature/'.length())
        def hash = featureName.hashCode().abs() % 10
        return 8090 + hash
    } else {
        return 8089
    }
}
