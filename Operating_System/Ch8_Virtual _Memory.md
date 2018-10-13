# Virtual Memory
* [重點一 : Virtual Memory](#重點一)
* [重點二 : Demand Paging](#重點二)
* [重點三 : Effective Access Time & Page fault ratio](#重點三)
* [重點四 : Paging](#重點四)
* [重點五 : Page Table 的製作](#重點五)
* [重點六 : Paging 之相關計算](#重點六)
* [重點七 : Structure of Page Table](#重點七)
* [重點八 : Segment Memory Management (Segmentation)](#重點八)
* [重點九 : Paged Segment Memory Management (分頁式分段)](#重點九)
* [重點十 : 小結](#重點十)

## 重點一
### Virtual Memory
* 主要目的 : 允許 process size > physical memory free space 情況下，程式仍然能執行。 
* 早期 : e.g. 使用 " overlay " 技術 ，是 Programmer 負擔 ( Dynamic loading )  
  現代 : O.S. 支援 Virtual Memory ( O.S 負擔 ) → Programmer 無須煩惱 
* 優點
  * [1] : 記憶體的各個小空間皆有機會被利用到，記憶體使用度上升。
  * [2] : 盡可能提高 multiprogramming degree，提升 CPU utiliztion ( Note : Thrashing 除外 )。
  * [3] : 每一次的 I/O transfer time 下降，因為不用將整個程式的所有 page 載入。
    * [註] 然而載入整個程式很耗費 I/O transfer time，因為總傳輸次數變多 ( I/O 次數,I/O time ↑)。

## 重點二
### 實現 Virtual Memoory 的技術之一 : Demand Paging 技術
* Def : Demand Paging 是架構在  Page Memory Management (Paging) 基礎上，採用 lazy swapper 技巧。即程式執行之初不將全部的 pages 載入 memory，僅載         入執行所須的 pages ( i.e. Prepaging )，甚至不載入 pages ( i.e. pure demand paging )，process 即可執行。  
  * 若 Process 執行所需之 Page 皆在 Memory ⇒ 正確執行
  * 若 Process 執行，企圖存取 " 不在 Memory 中之 Page " ⇒ 產生 " Page fault " interrupt ⇒ O.S 必須處理，載入 miss page 以利執行
* 在 Page Table 中多加一個 Valid/Invalid Bit 欄位，用以指示 page 是否在 memory 中。
  * V : 在 Memory 
  * I : 不在 Memory 
  * O.S : set & change 
  * MMU : reference only ( MMU 發出 interrupt )
  ![image](https://user-images.githubusercontent.com/38349902/46906980-540f2200-cf3e-11e8-9857-cf6e5b0d5545.png)
 
## 重點三
### Effective Access Time (EAT) in Vertual Memory & Page fault ratio 
* EAT = (1 – p) x memory access time + p x (page fault overhead + swap page out + swap page in + restart overhead)
  * p :  Page fault ratio 
* 欲提高 Virtual Memory 之效率，就是要降低 effective memory access time ( 降低 page fault ratio )
* 影響 Page fault ratio 之因素
  * [1] Page Replacement algo 之選擇
  * [2] Frame 數分配多寡之影響
  * [3] Page size 之影響
  * [4] Program Structure 之影響
  
  
  
  

       
