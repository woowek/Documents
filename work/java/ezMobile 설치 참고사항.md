ezMobile 설치 시 참고사항
===
>server.xml 관련
---
war 파일 압축 해제 경로
- /ezEKP/ezFlow/appBase 항목/docBase항목
- ex) /ezEKP/ezFlow/webapps2/ezMobile/ezMobile-1.0.0.war
```xml
<Host name="gwm.ekpkaoni.com"  appBase="webapps2" unpackWARs="false" autoDeploy="false">
	<Context path="" docBase="ezMobile" />
	<Valve className="org.apache.catalina.valves.AccessLogValve" directory="logs"
		prefix="localhost_access_log" suffix=".txt"
		pattern="%h %{X-Forwarded-For}i %D %t &quot;%r&quot; %s %b" />
</Host>
```
