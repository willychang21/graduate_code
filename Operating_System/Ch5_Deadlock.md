# Deadlock
### Deadlock
* (一) Def : Deadlock 意思是系統中存在一組 process 陷入互相等待對方所擁有的資源的情況(Circular Waiting)，造成所有的 process 無法往下執行，使得 CPU 利用度大幅降低，thoughput下降。
* (二) Deadlock 發生須符合以下 4 項充要條件
  * (1) Mutual exclusion：某些資源在同一個時間點最多只能被一個 process 使用
    * 例：大部分的 Resource 都是互斥使用 e.g. CPU,memory,I/O-device...
    * 例：Read-only File 不受互斥影響
  * (2) Hold and wait：某 process 持有部分資源，並等待其他 process 正在持有的資源
  * (3) No preemption：process 不可以任意強奪其他 process 所持有的資源
  * (4) Circular waiting：系統中存在一組 processes P=P0,P1,…,Pn，其中 P0 等待 P1 所持有的資源 ... Pn 等待 P0 所持有的資源，形成循環式等待。(因此，deadlock不會存在於single process環境中)
* (三) 例子
  * I. 系統面
  * II. 日常生活面
* (四) Deadlock v.s Starvation
![image](https://user-images.githubusercontent.com/38349902/47733226-bf792380-dca2-11e8-9f18-d8559c70280d.png)

### Deadlock 處理方法
Deadlock 三種處理方法：
* Deadlock prevention
* Deadlock avoidance
* Deadlock detection & recovery

prevention 和 avoidance
* 優點 : 保證系統不會進入 Deadlock  
* 缺點 : 資源利用度低，且可能有 Starvation 問題  

detection & recovery  
* 優點 : 資源利用度較高
* 缺點 : recovery cost 極高，且可能進入 Deadlock State  

#### 1.Deadlock prevention
打破必要條件四項之一，即可保證 Deadlock 永不發生。  
* 打破 Mutual exclusion：為資源與生俱來之性質，無法打破。
* 打破 Hold and wait：系統產能低。
  * [法一] 規定 : 除非 proces 可以一次取得所有工作所需的資源，才允許持有資源。
  * [法二] 規定 : process 執行之初可持有部分資源，但要再申請資源前，要先釋放手中所有資源。
* 打破 No Preemption : 改成 "Preemption" 即可，but 讓高優先 process 搶低優先 process 的資源，會造成 starvation。
* 打破 Circular waiting：Process須按照資源編號(unique resource ID)遞增(Ascending)方式申請資源。
#### 2.Deadlock avoidance





















