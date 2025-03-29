pipeline {
    agent any
    
    environment {
        SONAR_HOST = credentials('sonar-host-url')
        SONAR_TOKEN = credentials('sonar-token')
        DOTNET_CLI_HOME = "/tmp/dotnet_cli_home"
        BRANCH_NAME = "${env.BRANCH_NAME}"
        APP_PORT = calculatePortForBranch("${env.BRANCH_NAME}")
        DOCKER_NETWORK = "dotnet-apps-network"
    }
    
    stages {
        stage('Checkout') {
            steps {
                checkout scm
            }
        }
        
        stage('Setup Environment') {
            steps {
                // Create Docker network if it doesn't exist
                sh '''
                    if ! docker network inspect ${DOCKER_NETWORK} &>/dev/null; then
                        docker network create ${DOCKER_NETWORK}
                    fi
                '''
            }
        }
        
        stage('Build and Test') {
            agent {
                docker {
                    image 'mcr.microsoft.com/dotnet/sdk:7.0'
                    args '-v ${WORKSPACE}:/src -w /src'
                }
            }
            steps {
                sh 'dotnet restore'
                sh 'dotnet build --configuration Release --no-restore'
                sh 'dotnet test --no-restore --verbosity normal'
            }
        }
        
        stage('SonarQube Analysis') {
            agent {
                docker {
                    image 'mcr.microsoft.com/dotnet/sdk:7.0'
                    args '-v ${WORKSPACE}:/src -w /src'
                }
            }
            steps {
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
        
        stage('Publish') {
            agent {
                docker {
                    image 'mcr.microsoft.com/dotnet/sdk:7.0'
                    args '-v ${WORKSPACE}:/src -w /src'
                }
            }
            steps {
                sh 'dotnet publish -c Release -o ./publish'
            }
        }
        
        stage('Deploy with Docker') {
            when {
                anyOf {
                    branch 'main'
                    branch 'develop'
                    branch pattern: 'feature/*', comparator: 'GLOB'
                }
            }
            steps {
                script {
                    // Clean up existing container if it exists
                    sh """
                        CONTAINER_NAME="dotnet-app-${BRANCH_NAME.replaceAll('/', '-')}"
                        if docker ps -a | grep -q \$CONTAINER_NAME; then
                            docker stop \$CONTAINER_NAME || true
                            docker rm \$CONTAINER_NAME || true
                        fi
                    """
                    
                    // Deploy using Docker
                    sh """
                        docker run -d \\
                            --name dotnet-app-${BRANCH_NAME.replaceAll('/', '-')} \\
                            --network ${DOCKER_NETWORK} \\
                            -p ${APP_PORT}:80 \\
                            -v ${WORKSPACE}/publish:/app \\
                            -w /app \\
                            mcr.microsoft.com/dotnet/aspnet:7.0 \\
                            dotnet YourMainProject.dll
                    """
                    
                    // Show container info
                    sh """
                        echo "Container deployed:"
                        docker ps | grep dotnet-app-${BRANCH_NAME.replaceAll('/', '-')}
                    """
                }
            }
        }
    }
    
    post {
        always {
            cleanWs(cleanWhenNotBuilt: false,
                   deleteDirs: true,
                   disableDeferredWipeout: true,
                   notFailBuild: true)
        }
        success {
            echo "Build and deployment successful! Application is available at: http://server-ip:${APP_PORT}"
        }
        failure {
            echo "Build or deployment failed!"
        }
    }
}

// Function to calculate port based on branch name
def calculatePortForBranch(branchName) {
    if (branchName == 'main') {
        return 8080
    } else if (branchName == 'develop') {
        return 8081
    } else if (branchName.startsWith('feature/')) {
        // Generate port number between 8090-8099 based on feature name hash
        def featureName = branchName.substring('feature/'.length())
        def hash = featureName.hashCode().abs() % 10
        return 8090 + hash
    } else {
        // Default port for other branches
        return 8089
    }
}
