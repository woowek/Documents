- 내가 생각한 구성
    * scouter collector(collector, tomcat, host, scouter paper 설치)
    * tomcat(host 설치)

- collector server
    * 필요 사양 : JDK 1.8+
    * yum install tar
    * yum install wget
    * java 설치
        - yum install java-1.8.0-openjdk-devel.x86_64
    * 다운로드
        - wget https://github.com/scouter-project/scouter/releases/download/v2.20.0/scouter-all-2.20.0.tar.gz
    * tar -xvf scouter-all-2.20.0.tar.gz
    * 실행
        - /scouter/server/startup.sh
    * 사용 포트 : TCP 6100, UDP 6100
        - firewall-cmd --permanent --zone=public --add-port=6100/tcp
        - firewall-cmd --permanent --zone=public --add-port=6100/udp
        - firewall-cmd --reload
    * configuration
        - default : 
        - 설정파일 : /scouter/server/conf/scouter.conf
- host agent()
    * 필요 사양 : JDK 1.6+
    * 다운로드
        - wget https://github.com/scouter-project/scouter/releases/download/v2.20.0/scouter-all-2.20.0.tar.gz
    * 실행
        - /scouter/agent.host/host.sh

- tomcat agent(아래 개발서버 세팅에 적용)
    * 필요 사양 : JDK 1.6+
    * JVM 옵션 추가
        - ${TOMCAT_DIR}/bin/catalina.sh or startup.sh 에 옵션 추가
        ```
        JAVA_OPTS=" ${JAVA_OPTS} -javaagent:${SCOUTER_AGENT_DIR}/scouter.agent.jar"
        JAVA_OPTS=" ${JAVA_OPTS} -Dscouter.config=${SCOUTER_AGENT_DIR}/conf/scouter1.conf"
        JAVA_OPTS=" ${JAVA_OPTS} -Dobj_name=myFirstTomcat1"
        ```


- 개발서버 세팅
    * Dockerfile을 수정한다.
        - scouter 폴더를 복붙
    * kubernetes 환경에서 실행한거라 docker 파일을 수정한다.
        - 환경변수 추가
        - host.sh 실행구문 추가
        ```
        ENV JAVA_HOME=/usr/local/java
        ENV PATH=/usr/local/java/bin:$PATH
        ENV JAVA_OPTS=" -javaagent:/scouter/agent.java/scouter.agent.jar -Dscouter.config=/scouter/agent.java/conf/scouter.conf -Dobj_name=ezportal"

        USER jmocha

        ENTRYPOINT ["/bin/bash", "-c"]
        CMD ["/entrypoint.sh && /scouter/agent.host/host.sh"]
        ```




- scouter paper
    * 사이트 : https://scouter-contrib.github.io/scouter-paper/manual.html
    * https://github.com/scouter-contrib/scouter-paper/releases 에서 다운로드
    * yum install unzip
    * http://아이피:6188/extweb/index.html
    * 사용 포트 : TCP 6188, 6180
        - firewall-cmd --permanent --zone=public --add-port=6188/tcp
        - firewall-cmd --permanent --zone=public --add-port=6180/tcp
        - firewall-cmd --reload
    * /root/scouter/webapp/extweb 폴더에 압축풀기
        - unzip scouter-paper-v2.6.4.zip
    * /root/scouter/webapp/startup.sh 실행
    * annotation 수정
    ```
    net_http_server_enabled=true
    net_http_api_enabled=true
    net_http_port=6180
    ```
    * 버전이슈로 재구성..
        - 최신버전(2.20.0)은 연계가 되지 않는다.
        - 2.15버전까지 지원이 되는거같다.
        - wget https://github.com/scouter-project/scouter/releases/download/v2.15.0/scouter-all-2.15.0.tar.gz
