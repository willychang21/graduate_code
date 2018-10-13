# Virtual Memory
* [重點一 : Virtual Memory](#重點一)
* [重點二 : Demand Paging](#重點二)
* [重點三 : Fragmentation](#重點三)
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
## Demand Paging 技術
