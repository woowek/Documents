maven 빌드 시 오류 대처
===
>pom.xml 의 repository url 변경
---
http -> https 로 변경
```xml
		<repository>
			<id>mvn2</id>
			<url>http://repo1.maven.org/maven2/</url>
			<releases>
				<enabled>true</enabled>
			</releases>
			<snapshots>
				<enabled>true</enabled>
			</snapshots>
		</repository>
		<repository>
			<id>egovframe</id>
			<url>https://www.egovframe.go.kr/maven/</url>
			<releases>
				<enabled>true</enabled>
			</releases>
			<snapshots>
				<enabled>false</enabled>
			</snapshots>
		</repository>
		<repository>
			<id>egovframe2</id>
			<url>http://maven.egovframe.kr:8080/maven/</url>
			<releases>
				<enabled>true</enabled>
			</releases>
			<snapshots>
				<enabled>false</enabled>
			</snapshots>
		</repository>
```