# Virtual Memory
* [重點一 : Virtual Memory](#重點一)
* [重點二 : Demand Paging](#重點二)
* [重點三 : Effective Access Time & Page fault ratio](#重點三)
* [重點四 : Page Replacement](#重點四)
* [重點五 : Page Replacement algo.](#重點五)
   * [FIFO](#ＦＩＦＯ)
   * [OPT](#ＯＰＴ)
   * [LRU](#ＬＲＵ)
   * [LRU近似法則](#ＬＲＵ近似法則)
   * [LRU & MFU](#ＬＲＵ＆ＭＦＵ)
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

## 重點四
### Page Replacement
* Def : 當 page fault 發生且 memory 無可用 frame 時，則 OS 必須執行 page replacement。OS 必須選擇一個 victim page，將其 swap out 到 disk 來空         出一個 free frame，再將 missed page swap in 到此 frame。
![image](https://user-images.githubusercontent.com/38349902/46912343-7be5a080-cfa5-11e8-92f9-5dae7a597c0d.png)
* swap out 和 swap in 分別是 2 個 disk I/O 的動作 → Page process time 更長。
* swap in 是必要的，一定要將 missed page 置入到 memory 中。
* 解法 : 引入 dirty bit ( Modification bit ) 到 Page Table 決定是否 swap out
* dirty bit
  * 用於表示 victim page 是否曾被修改過 
    * 0 : 沒有 ⇒ 不須被 swap out，省下此次 I/O 
    * 1 : 有 ⇒ swap out
    * MMU : set ( 0 → 1 )
    * O.S : reset ( 1 → 0 ) & reference
* Page Replacement policy
  * Local Replacement 
    * Def : O.S 在 select victim page 時，只能從該 process 之 frames 中挑選
    * 優點 : 降低 thrashing 機率
    * 缺點 : Memory utilization 較差 ( 其他 process 有 free frame 卻不能用 )
  * Global Replacement 
    * Def :　O.S 在 select victim page 時，能挑選其他 process 之 page 
    * 優點 : Memory utilization 較好
    * 缺點 : 提高 thrashing 機率
    
## 重點五
### Page Replacement algo.
#### ＦＩＦＯ
* Def : 最先載入的 page 優先視為 victim page。
* 分析
  * [1] 簡單，易於實作 
  * [2] 效果不是很好，page fault ratio 相當高
    * 但不代表是最差的 (在 page replacement 沒有最差，因為無法預知未來，只有一個 OPT 是最佳)
  * [3] 可能有 ⇒ Belady Anomaly(異常現象)：當Process分配到較多的Frame數目其Page Fault Ratio不降反升。
#### ＯＰＴ
* Def : 以將來長期不會使用的 page 視為 victim Page。 
* 分析
  * [1] 效果最佳，page fault ratio 最低 
  * [2] 具備 Stack Property ⇒ 不會有 Belady Anomaly
    * Stack Property : n 個 frame 所包含的 page set 一定是 (n+1) 個 frame 之 page set 的 subset
      * 凡是具備 Stack property 者，絕不會發生 Belady Anomaly，反之則有可能發生 (只有 OPT & LRU 具備) 
  * [3] 無法實作出，因為是看未來！不過可以知道 upper bound。
#### ＬＲＵ
* Def : 以最近不常使用的 page 視為 victim page。
* 分析
  * [1] 效果不錯，Page fault ratio尚可接受
  * [2] 具備 Stack Property ⇒ 不會有 Belady Anomaly
  * [3] 製作成本高，需要大量硬體支援(如:需要Counter或Stack) 
* LRU製作方法
  * [1] Counter
    * Def : 利用 Counter 作一個 logical timer，當發生 memory acess 時
      * (1) counter ++ (時鐘往前進)
      * (2) copy counter 值到參考 page 之 " the last reference time " 欄位，將來挑 the last reference time 最小的 page (離現在最久)，及為 LRU             page
  * [2] Stack 
    * Def : 目前最近的參考 page 會被置於 Stack 的 Top 端，而 Stack 之 Bottom 端，即為 LRU page，而 Stack size = frame 數目
 #### ＬＲＵ近似法則
 由於 LRU 製作成本過高，因此產生以下方法近似 LRU 效果。這些近似方法都有可能退化成 FIFO，會遇到 Belady 異常情況。
 * 基礎 : 在 Page Table 中多加一個欄位 : " Reference Bit " 代表 Page 有無被參考過(R.W)
   * 1 : 有
   * 0 : 無
   * MMU : set ( 0 → 1 )
   * O.S : reset ( 1 → 0 ) & reference
 * [法一] Addition Reference Bit Usage
   * 作法 : 每個 Page 皆有一個 register (e.g. 8 bits )，保存此 Page 最近幾次的 reference bit 值，每隔一段時間系統會
     * (1) 將 Page 的 reg. 均右移一位(最右元捨去，空出左位元)
     * (2) copy 每個 page 之 ref. bit 到 reg. 的最左位元 (剛空出來的)
     * (3) page 之 ref. bit 值 reset 0
     * (4) 將來要挑選 victim page 時，挑 reg.值(unsigned)最小的 page 為 victim 若有多個 page 具相同值，以 FIFO 為準
 * [法二] Second Chance ( Clock algo )
   * 作法 : 以 FIFO 法則為基礎，搭配 Reference Bit 使用。
     * (1) 先以 FIFO order 選出一個 Page
     * (2) check 此 Page 的 reference bit (每個 page 的初始值 reference bit 值設為 1)
       * 若為 1
         * (i) 給他機會 (不挑他為 Victim )
         * (ii) ref. bit 清為 0
         * (iii) 改它的 loading time 為現在時間
         * (iv) goto (1)
       * 若為 0 : 則它是 Victim Page
 * [法三] Enhance Second Chance
   * 作法 : 以 < Reference Bit, Modification Bit > 作為挑 Victim Page 依據，值最小的 Page 作為 Victim，若多個 Page 具相同值，則以 FIFO 為主

#### ＬＲＵ＆ＭＦＵ
       
 









|                  |  FIFO  |    OPT   |     LRU    | LRU&MFU |
|:----------------:|:------:|:--------:|:----------:|:-------:|
| Page fault ratio | 相當高 |   最低   |  尚可接受  |   很高  |
|       效果       |  不好  |   最佳   |    還行    |   不好  |
|  Stack Property  |    ×   |     √    |      √     |    ×    |
|  Belady Anomaly  |    √   |     ×    |      ×     |    √    |
|       NOTE       |        | 無法實作 | 硬體成本高 |  成本高 |
  

  
  
  
  

       
