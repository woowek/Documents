git 명령어
===


자료출처

https://evan-moon.github.io/2019/07/25/git-tutorial/

>clone
---
clone은 말 그대로 리모트 서버의 레파지토리에서 클라이언트로 파일을 복붙하는 행위를 말한다. 
```bash
$cd ~/dev/evan # 원하는 작업 디렉토리로 이동
$git clone https://github.com/evan-moon/test-repo.git
```
>pull
---
pull 명령어는 리모트 서버의 최신 소스를 가져와서 로컬 소스에 병합(Merge)해주는 명령어
```bash
$git pull # 현재 내 로컬 브랜치와 같은 이름을 가진 리모트 서버 브랜치가 타겟
$git pull origin master # origin 리모트 서버의 master 브랜치가 타겟
```
>fetch
---
fetch는 리모트 서버의 최신 이력을 내 클라이언트로 가져오되 병합은 하지 않는 명령어이다.
```bash
$git fetch
```

>add
---
```bash
$git add . # 현재 디렉토리의 모든 변경사항을 스테이지에 올린다
$git add ./src/components # components 디렉토리의 모든 변경사항을 스테이지에 올린다
$git add ./src/components/Test.vue # 특정 파일의 변경사항만 스테이지에 올린다
$git add -p # 변경된 사항을 하나하나 살펴보면서 스테이지에 올린다
```

>commit
```bash
$git log --graph
```

>push
---
커밋을 통해 포장된 변경 사항들은 push 명령어를 사용하여 리모트 서버로 업로드 된다. 
```bash
$git push origin master # origin 리모트 서버의 master 브랜치로 푸쉬해줘!
```
Git은 브랜치를 자동으로 추적할 수 있는 기능 또한 제공해준다.
```bash
$git push --set-upstream origin master
```
<br>
<br>
<br>
<br>
자료출처

https://evan-moon.github.io/2019/07/28/git-tutorial-advanced/
>Merge
---
머지(Merge)는 제일 기본적인 브랜치 병합 기능으로, 합치려고 하는 대상 브랜치의 변경 사항을 타겟 브랜치에 모두 반영하면서 머지 커밋(Merge commit)을 남긴다.
```bash
$git checkout master
$git merge feature
```
>>Merge squash

이번에는 두 개의 브랜치를 병합할 때 사용하는 머지 명령어의 --squash 옵션을 한번 알아보자.
--squash 옵션은 해당 브랜치의 커밋 전체를 통합한 커밋을 타겟 브랜치에 머지하는 옵션이다.
```bash
$git checkout master
$git merge --squash feature
```

>Rebase
---
리베이스(Rebase)도 머지(Merge)와 마찬가지로 브랜치를 다른 브랜치로 합칠 수 있는 기능이다. 단 머지와 차이가 있다면 바로 합치는 방식이다. 
```bash
$git rebase master
```

>Cherry Pick
---
체리픽(Cherry Pick)은 다른 브랜치에서 어떤 하나의 커밋만 내 브랜치로 가져오는 기능이다.
```bash
$git checkout master
$git cherry-pick 35058b4   #가져올 커밋 해쉬
```
>Reset
---
리셋(Reset)은 지정한 커밋 당시로 돌아가는 것이다.
```bash
$git reset --hard c04f8f6
```
 * hard: 지정한 커밋 이후의 히스토리가 삭제되고 삭제된 내용들은 그대로 사라진다.
 * soft: 지정한 커밋 이후의 히스토리가 삭제되고 삭제된 내용들은 스테이지로 이동한다.(add한 상태로 변경)
 * mixed: 지정한 커밋 이후의 히스토리가 삭제되고 삭제된 내용들은 스테이지에 올라가지 않은 상태가 된다.(다시 add 해줘야 함)

>Revert
---
리벗(revert) 또한 리셋처럼 히스토리를 다시 되돌리고 싶을 때 사용하는 명령어이다.
리셋이 지정한 커밋 이후의 모든 히스토리를 없애버렸다면 리벗은 특정 커밋의 변경 사항을 되돌리는 기능이다.
```bash
$git revert 35058b4    #특정 커밋을 되돌린다
$git revert 35058b4..c04f8f6   #커밋의 범위를 지정하여 되돌린다
$git revert HEAD   #현재 헤드가 위치한 커밋을 되돌린다
```