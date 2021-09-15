gitignore 설정
===

>.gitignore 등록
---
- .git 폴더가 있는 경로에 .gitignore 파일 생성 후 내용 작성


>.gitignore 예시
---
```txt
# 특정 파일 fileName 제외
fileName.js

# 현재 경로에 있는 fileName_1 만 제외
/fileName.js

# 특정 폴더 node_module 안의 파일 제외
node_module/

# 특정 경로의 특정 파일 제외
folder/my.txt 

# 특정 경로 아래의 모든 fileName 제외
folder/**/fileName.txt 

# 특정 확장자 파일 모두 제외
*.txt 

# 예외 만들기
!fileName.txt
```



>.gitignore가 작동하지 않을때
---
- git 캐시 제거 후 커밋
```bash
$git rm -r --cached .
$git add .
$git commit -m "fixed untracked files"
$git push
```