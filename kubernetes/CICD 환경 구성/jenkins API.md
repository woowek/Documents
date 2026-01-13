- jenkins의  콘솔에서의 처리가 필요하다..

- 일단 설치를 한다.
    * helm install jenkins ./jenkins -n jenkins -f jenkins-values.yaml
    * jenkins-values.yaml 여기에 후술할 cloud, credential 설정이 들어갈거다.



- 필요한건 다음과 같다.
    * cloud설정에 kubernetes 등록
    * credential 추가
        - gitea 계정/PW
        - gitea Token
        - harbor 계정/PW
    * security에 Authentication에 Security Realm 추가(keycloak)
    * pipeline 생성 및 호출


- 플러그인 자동배포를 시도해봤는데..... 몇가지 이유로 포기했다.
    * 우선 pv로 설정하기엔 모든 노드에 플러그인 파일을 넣어주어야 한다.
    * 플러그인 파일에 변경사항이 생긴경우 원래 폴더를 변경하지 않았을 경우 롤백이된다.
    * helm 내부에서는 지원 용량이 부족하다.
    * initContainer를 추가하기엔 무리가 있다...
- 그래서 다음 방법을 시도해본다.
    * pod에 파일을 직접 이동한다.
    * 직접 선택한 플러그인 목록이다.(순서가 중요한가보다...)
        - Kubernetes
        - Gitea
        - Keycloak Authentication
        - Generic Webhook Trigger
        - Docker Pipeline
        - Maven Integration
        - Pipeline
        - Pipeline Maven Integration
        - Pipeline: Stage View
        - Plain Credentials
        - Configuration as Code


    * kubectl rollout restart statefulset -n jenkins jenkins 명령어로 jenkins를 재기동한다. 

    


- 일단 클라우드 설정을 하려면 플러그인이 설치되어있어야 한다. 먼저 PV에 플러그인 파일을 넣자.
    * 추가로 Configuration as Code 플러그인이 필요하다.
    * 일단 helm 설치 후 플러그인만 적용한 상태를 확인해보자..
        - cloud 설정
        - credential

