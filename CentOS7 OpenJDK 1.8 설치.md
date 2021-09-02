open-jdk 1.8 설치
===

>open-jdk 1.8 설치
---
```bash
$yum install java-1.8.0-openjdk
$yum install java-1.8.0-openjdk-devel
```

>환경변수 등록
---
```bash
$readlink -f /usr/bin/java/usr/lib/jvm/java-1.8.0-openjdk-1.8.0.242.b08-0.el7_7.x86_64/jre/bin/java
$vi /etc/profile
```
```
JAVA_HOME=/usr/lib/jvm/java-1.8.0-openjdk-1.8.0.242.b08-0.el7_7.x86_64
PATH=$PATH:$JAVA_HOME/bin
CLASSPATH=$JAVA_HOME/jre/lib:$JAVA_HOME/lib/tools.jar
```

```bash
$export JAVA_HOME PATH CLASSPATH
```


>환경변수 확인
---
```bash
$echo $JAVA_HOME
$echo $PATH
$echo $CLASSPATH
```