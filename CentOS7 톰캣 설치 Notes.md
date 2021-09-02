Tomcat 서비스 설치
===

>다운로드
---
```bash
$wget http://archive.apache.org/dist/tomcat/tomcat-8/v8.5.66/bin/apache-tomcat-8.5.66.tar.gz
```

>압축 해체
---
```bash
$tar zxvf apache-tomcat-8.5.27.tar.gz
```

>톰캣을 /usr/local/로 이동시키고 디렉토리 이름을 tomcat8로 변경
---
```bash
$mv apache-tomcat-8.5.27 /usr/local/tomcat8
```


>아래 설정을 찾아서 URIEncoding="UTF-8"을 추가한다.
---
```bash
$vi /usr/local/tomcat8/conf/server.xml
```
```xml
<Connector port="8080" protocol="HTTP/1.1"
            connectionTimeout="20000"
            redirectPort="8443"
            URIEncoding="UTF-8" />
```

>환경변수 변경
---
```bash
$vi /etc/profile #자바 버전에 맞게 등록
```
```console
JAVA_HOME=/usr/lib/jvm/java-1.8.0-openjdk-1.8.0.242.b08-0.el7_7.x86_64/
CATALINA_HOME=/usr/local/tomcat8
CLASSPATH=$JAVA_HOME/jre/lib:$JAVA_HOME/lib/tools.jar:$CATALINA_HOME/lib-jsp-api.jar:$CATALINA_HOME/lib/servlet-api.jar
PATH=$PATH:$JAVA_HOME/bin:/bin:/sbin
export JAVA_HOME PATH CLASSPATH CATALINA_HOME
```

>설정 후 아래 명령어 입력 
---
```bash
$source /etc/profile
```

>
---tomcat 실행
```bash
$/usr/local/tomcat8/bin/startup.sh
```

>톰캣 프로세스 확인
---
```bash
$ps -ef|grep tomcat8
```

>8080 포트가 열려있는지 확인 
---
```bash
$netstat -tln
```

>테스트
---
```bash
$wget http://localhost:8080/
```

>systemctl 등록
---
```bash
$vi /etc/systemd/system/tomcat8.service  #자바 버전에 맞게 등록
```
```console
# Systemd unit file for tomcat
[Unit]
Description=Apache Tomcat Web Application Container
After=syslog.target network.target

[Service]
Type=forking

Environment="JAVA_HOME=/usr/lib/jvm/java-1.8.0-openjdk-1.8.0.242.b08-0.el7_7.x86_64/"
Environment="CATALINA_HOME=/usr/local/tomcat8"
Environment="CATALINA_BASE=/usr/local/tomcat8"
Environment="CATALINA_OPTS=-Xms512M -Xmx1024M -server -XX:+UseParallelGC"
Environment="JAVA_OPTS=-Djava.security.egd=file:///dev/urandom"

ExecStart=/usr/local/tomcat8/bin/startup.sh
ExecStop=/usr/local/tomcat8/bin/shutdown.sh

User=root
Group=root
UMask=0007
RestartSec=10
Restart=always

[Install]
WantedBy=multi-user.target
```

```bash
$systemctl daemon-reload
$systemctl enable tomcat8  #tomcat8 실행
$systemctl start tomcat8
```



>tomcat manager 설정 (해당 설정은 선택사항 입니다.)
---
>>tomcat-users.xml 수정
```bash
$vi /usr/local/tomcat8/conf/tomcat-users.xml
```
```xml
<tomcat-users xmlns="http://tomcat.apache.org/xml" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xsi:schemaLocation="http://tomcat.apache.org/xml tomcat-users.xsd" version="1.0">
  <role rolename="manager"/>
  <role rolename="manager-gui" />
  <role rolename="manager-script" />
  <role rolename="manager-jmx" />
  <role rolename="manager-status" />
  <role rolename="admin"/>
  <user username="admin" password="패스워드" roles="admin,manager,manager-gui, manager-script, manager-jmx,  manager-status"/>
</tomcat-users>
```
>>외부 접근 허용 설정
```bash
$vi /usr/local/tomcat8/conf/Catalina/localhost/manager.xml
```
```xml
<Context privileged="true" antiResourceLocking="false" docBase="${catalina.home}/webapps/manager">
          <Valve className="org.apache.catalina.valves.RemoteAddrValve" allow="^.*$" />
</Context>
```
>>테스트

http://자신의IP:8080/manager