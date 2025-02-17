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
    * 일단 sonarqube scanner 플러그인을 설치한다..
    * jenkins에서 sonarqube 서버를 등록하고
    * jenkins의 credential도 추가한다.
    * 파이프라인 등록. 뭐 이런식으로 하라는데 솔직히 잘 모르겠다..직접 해봐야알듯
    ```sh
    stage('SonarQube analysis') {
    steps {
        withSonarQubeEnv('SonarQube-server') {
            sh 'mvn sonar:sonar'
        }
    }
    ```