MariaDB 설치
=========

>MariaDB 설치
-----
```bash
$sudo yum install mariadb-server 
$sudo systemctl start mariadb (마리아DB 실행)
$systemctl enable mariadb(시작프로그램 등록)
```

>보안 관련된 부분을 설정하는 명령을 사용합니다.
----
```bash
$mysql_secure_installation
```


>방화벽 설정
---
```bash
$firewall-cmd --zone=public --permanent --add-port=3306/tcp(3306 포트 등록)
$firewall-cmd --reload
```



>root 접속 허용
---
```bash
$mysql -u root {패스워드}
```
```sql
set password=password('넣고자하는패스워드');
grant all privileges on*.* to ‘계정’@'%' identified by ’패스워드’;
```