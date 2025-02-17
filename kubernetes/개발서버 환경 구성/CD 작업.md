- CD 작업도 나중엔 해야해서 결국 작업을 해버렸다.
- CD 작업은 argo CD로 작업
- 생각보다 어렵진 않았다. jenkins때 머리아플때보단 많이 쉬웠다.
- 작업내용
    1. 우선 ArgoCD의 Project에 git repository를 추가했다.
    2. 그리고 Application에 kubernetes namespace - git repository 를 추가해준다.
        * 이번 작업한 거에는 deploy만 작업을 했지만 다른 resource 도 작업 가능할거같다.
        * git에 kubernetes에 적용할 내용들을 추가해줘야한다.
            - 이전 작업때는 작업 소스가 git에 같이 들어있어 배포 전용 git repository가 필요했었던거같다. 그러나 이번엔 일반 소스와 배포 소스가 다르기 때문에 그냥 바로 배포가 된다.
            - argocd의 path정보에 해당하는 경로를 git에 추가한다. 그 후 해당 경로에 배포할 정보를 추가한다. 이번 작업 같은 경우 deploy만 배포 목적이었으므로 deploy 기본정보만 등록했다.
    3. jenkins script 작성
        * 이전엔 container를 추가하고, kustomize 등록해서 뭐 이것저것했는데 조사해보니까 다 필요없더라..
        * argocd의 path 내부의 deploy만 수정하고 git push하는 내용만 추가했다.
        ```groovy
        script {
            withCredentials([[$class: 'UsernamePasswordMultiBinding',
                credentialsId: 'gitlab-credentials',
                usernameVariable: 'USERNAME',
                passwordVariable: 'PASSWORD'
            ]]) {
                dir("/home/jenkins/agent/workspace/${packageName}/base"){
                    sh """
                    sed -i -e 's/image: nexus.kaoninfra.com:31110\\/${microServiceName}:[0-9]\\{8\\}bn[0-9]\\+/image: nexus.kaoninfra.com:31110\\/${microServiceName}:${today}bn${env.BUILD_NUMBER}/g' deployment.yaml
                    git checkout main
                    git config --global user.email \"jenkins@kaoniDevops.com\"
                    git config --global user.name \"Jenkins\"
                    git add .
                    git commit -m \"${microServiceName}:${today}bn${env.BUILD_NUMBER} deploy\"
                    git push http://${USERNAME}:${PASSWORD}@gitlab.kaoninfra.com/ezekp365g/${microServiceName}.git
                    """
                }
            }
        }
        ```
        