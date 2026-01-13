- sonarqube도 helm으로 배포한다.
```
helm repo add sonarqube https://SonarSource.github.io/helm-chart-sonarqube
helm repo update
kubectl create namespace sonarqube
export MONITORING_PASSCODE="yourPasscode"
helm upgrade --install -n sonarqube sonarqube sonarqube/sonarqube --set edition=developer,monitoringPasscode=$MONITORING_PASSCODE
```
- monitoringPasscode 이건 어디에 쓰는건지 잘 모르겠다...
- 일단 이정도면 될거같긴 한데 values는 알아서 봐야하겠지..

- sonarqube-jenkins 연동처리
    * sonarqube
        - 사용자 메뉴에서 security 탭에서 token을 발급한다. 이건 jenkins의 credential에 적용한다.
        - 프로젝트를 manual 형태로 추가한다.
        - project 키와 name을 등록한다.
    * jenkins
        - 일단 sonarqube scanner 플러그인을 설치한다..
        - jenkins의 credential을 추가하고(나는 sonarqube의 관리자계정 토큰을 secret Text로 추가해서 등록했다.)
        - jenkins에서 sonarqube 서버를 등록한다. credential은 이전에 등록한걸로 등록한다.
        - 파이프라인 등록. 뭐 이런식으로 하라는데 솔직히 잘 모르겠다..직접 해봐야알듯
        ```sh
        stage('SonarQube analysis') {
        steps {
            withSonarQubeEnv('SonarQube-server') {
                sh 'mvn sonar:sonar'
            }
        }
        ```


- 정책 적용
    * quality profiles 에서 룰 추가
        - 테스트로 java의 sonarQubeTest로 추가함
        - 해당 정책에서 설정버튼의 active more rules 로 정책 추가.
    * SonarQube 행안부 보안취약점 대응표
        - https://confluence.curvc.com/pages/viewpage.action?pageId=223118213
        - 여기서 알려주는 정책 중 일부가 없음 이건 확인 필요
    * 


- 행안부 보안 가이드라인 등록
    * 행안부 보안 가이드라인대로 일일이 등록을 해야할것처럼 보인다.

