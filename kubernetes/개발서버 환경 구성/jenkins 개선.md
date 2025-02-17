- jenkins 작업을 했는데 개선이 필요하다..
- 일단 개선 필요사항은 git와 docker를 PV로 구성해야할거같다,..

- 결론부터 말하면 구성은 완료했으나 속도가 답이 안나올정도로 느리다. 리소스 문제는 아닌거같고 Disk IO 때문인거같은데 이상하다.
  일단 나중에 다시 쓰게될 수 있으니 기록을 한다.


- 현재 구성 
    * masterNode01 -> jenkins -> container의 형태이며 하위 container는 volumes에서 마운트된걸 사용한다.

- 추가 구성
    * 이미 jenkins pod에 pv(/var/jenkins_home)가 있다. pipeline에도 이 pv를 mount했다(/home/jenkins/volume). 그러니까 jenkins 의 /var/jenkins_home 경로는 pv이므로 여기에 폴더를 만들고 git /Docker 처리를 해보자
      - git : /var/jenkins_home/devgit(jenkins) -> /home/jenkins/volume/devgit(container)
      - docker : /var/jenkins_home/dockerlayer(jenkins) -> /home/jenkins/volume/dockerlayer(container)
    * docker에는 layer를 저장할 경로를 별도지정했다. arg에 '--data-root=/home/jenkins/volume/dockerlayer' 를 추가해 jenkins pipeline에서 docker build를 할 때 해당 경로로   layer가 저장되도록 했다.
    * 작성한 podTemplate는 다음과 같다.
    ```groovy
    podTemplate(
      containers: [
          containerTemplate(name: 'jnlp'
              , image: 'jenkins/inbound-agent:latest'
              , resourceRequestMemory: '512Mi'
              , resourceRequestCpu: '500m'
              , workingDir: '/home/jenkins/agent'
          )
          ,containerTemplate(name: 'docker'
              , image: 'docker:24-dind'
              , resourceRequestMemory: '2Gi'
              , resourceRequestCpu: '500m'
              , ttyEnabled: true
              , command: 'dockerd-entrypoint.sh'
              , privileged: true
              , args: '--data-root=/home/jenkins/volume/dockerlayer --insecure-registry=넥서스IP:포트'
          )
          ,containerTemplate(name: 'maven'
              , image: 'maven:3.8.8-ibmjava-8'
              , resourceRequestMemory: '256Mi'
              , resourceRequestCpu: '500m'
              , ttyEnabled: true
              , command: 'cat'
          )
      ],
      volumes: [
          persistentVolumeClaim(mountPath: '/home/jenkins/volume', claimName: 'jenkins-pvc'),
          persistentVolumeClaim(mountPath: '/root', claimName: 'maven-jenkins-pvc'),
          secretVolume(secretName: 'docker-config', mountPath: '/etc/docker')
      ]
    )
    ```

    * pipeline script는 해당 연동 처리 경로에 들어가 git pull 처리, docker build는 이전과 동일하다.
