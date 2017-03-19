

node("swarm&&aspnet-core") {
    stage("checkout") {
        checkout scm
    }

    def dockerRepositoryHost = "registry.mooggaming.net"
    def gitCommit = sh(returnStdout: true, script: "git rev-parse --short HEAD").trim()
    def imageName = "${dockerRepositoryHost}/socvr/socvr-website-server"

    //right now I'm not able to output the unit tests to a file that can be sent to jenkins.

    stage('building image') {
        dir('SOCVR.Website.Server') {
            sh "dotnet restore"
            sh "dotnet publish --configuration Release --output publish"
            sh "docker build -t ${imageName}:build-${gitCommit} ."

            if(env.BRANCH_NAME == "master") {
                sh "docker tag ${imageName}:build-${gitCommit} ${imageName}:latest"
            }
        }
    }
    
    stage('pushing image') {
        withCredentials([usernamePassword(
            credentialsId: 'a156757a-8935-49a8-913d-30c02db35d60', 
            passwordVariable: 'jenkinsDockerRegistry_password', 
            usernameVariable: 'jenkinsDockerRegistry_username')]) {

            //need to login to get the base image
            sh "docker login -u '${jenkinsDockerRegistry_username}' -p '${jenkinsDockerRegistry_password}' ${dockerRepositoryHost}"
        }

        sh "docker push ${imageName}:build-${gitCommit}"

        if(env.BRANCH_NAME == "master") {
            echo "pushing latest"
            sh "docker push ${imageName}:latest"
        }
    }
}
