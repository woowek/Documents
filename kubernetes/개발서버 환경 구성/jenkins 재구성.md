1.  현재 구성
    - 지금 jenkins 돌아가는 형태는 다음과 같다.
    - git checkout -> maven build() -> docker build -> Nexus push


2. 나는 이를 이렇게 바꿔볼 생각이다...
    - git checkout & pull -> maven build() -> docker build -> Nexus push


3. 첫 시도
    - 빌드할 내용을 기본 git로 두고, 내부에 소스를 git로 또 구성했는데 내부 git은 jenkins애서 git으로 인식하지 않았다....

4. 테스트..
    - git subtree와 git sunmodule로 테스트해볼 생각이다.XXXXXXXXXXXXXX
    - 둘다 문제다. 그냥 전체 소스를 clone 했다...생각보다 그리 안느리더라....
        <!-- - 우선 조건을 생각해보자..
            * 내부 소스는 only pull만 진행할거다.
            * 외부 소스는 따로 관리한다.
        - 먼저 git subtree(이방법이 맞을거같긴 하다..)
            * git 최상위 경로에서 git subtree add --prefix=<디렉토리> <저장소 URL> <브랜치>
            * 작업 URL : git subtree add --prefix=source/ezApprovalG jmocha@jgit.kaoni.com:msa/ezApprovalG.git csap_dev
            * git subtree pull --prefix=<디렉토리> <저장소 URL> <브랜치>
            * 작업 URL : git subtree pull --prefix=source/ezApprovalG jmocha@jgit.kaoni.com:msa/ezApprovalG.git csap_dev
        - 제거는 다음과 같다.
            * git rm -rf src/ezApprovalG -> 이거 하면 다시 처음부터 해야한다... -->
    
    - ssh 도 문제다.
        * jenkins 서버 계정의 .ssh 폴더에 config 파일로 아래 내용을 넣었다.
        ```
        Host jgit.kaoni.com
            User jmocha
            Port 22
            HostkeyAlgorithms +ssh-rsa
        ```
        * 그 후 키 생성, private는 jenkins서버에, public key는 git 서버에 저장한다.
            - ssh-keygen -t rsa
            - jenkins credential 에 private key 추가
            - 대상 서버의  .ssh 경로의 authorized_keys 파일에 public key를 추가한다.

    - git이 구버전이라 별도의 ssh설정 구문을 추가해야했다...
    ```
    sh '''
    mkdir -p ~/.ssh
    echo "Host jgit.kaoni.com" >> ~/.ssh/config
    echo "    User jmocha" >> ~/.ssh/config
    echo "    Port 22" >> ~/.ssh/config
    echo "    HostKeyAlgorithms +ssh-rsa" >> ~/.ssh/config
    echo "    PubkeyAcceptedKeyTypes +ssh-rsa" >> ~/.ssh/config
    chmod 600 ~/.ssh/config
    '''
    ```

    - host 정보도 추가해야한다.
    ```
    sh '''
    ssh-keyscan -H jgit.kaoni.com >> ~/.ssh/known_hosts
    chmod 600 ~/.ssh/known_hosts
    '''
    ```
