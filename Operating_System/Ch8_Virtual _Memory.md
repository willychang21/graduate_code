# Virtual Memory
* [重點一 : Virtual Memory](#重點一)
* [重點二 : Demand Paging & Copy on Write](#重點二)
* [重點三 : Effective Access Time & Page fault ratio](#重點三)
* [重點四 : Page Replacement](#重點四)
* [重點五 : Page Replacement algo.](#重點五)
   * [FIFO](#ＦＩＦＯ)
   * [OPT](#ＯＰＴ)
   * [LRU](#ＬＲＵ)
   * [LRU近似法則](#ＬＲＵ近似法則)
   * [LRU & MFU](#ＬＲＵ與ＭＦＵ)
   * [Prepaging](#Ｐｒｅｐａｇｉｎｇ)
   * [比較表](#比較表)
* [重點六 : Page Buffering 機制](#重點六)
* [重點七 : Free Frame 分配多寡對 page fault ratio 之影響](#重點七)
* [重點八 : Thrashing](#重點八)
* [重點九 : Working Set Model](#重點九)
* [重點十 : Page Size 對 Page Fault Ratio 之影響 (TLB reach)](#重點十)
* [重點十一 : Page Structure 對 Page Fault Ratio 之影響](#重點十一)
* [重點十二 : 記憶體映射檔案 Memory-Mapped Files](#重點十二)

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

### Copy on Write
[型一] 傳統的 fork() (in Ch4. fork() without copy-on-write) 
* Def : parent 生出 child process 後，child process 佔用與 parent 不同的 memory space 且 child 的 Code section 與 Data section 內容均來自           parent 之 Copy initially 
* 缺點 
  * [1] child process 需要被配置 New frames ( memory space )
    * 若 child process 生成數目眾多 ⇒ 很耗 Memory space
  * [2] Copy parent process 之 Code/Data section 內容給 child process 很耗時，而且有時候是不必要的 ( if. child 立刻執行 execlp())
[型二] fork() with Copy on write 技術
copy-on-write (COW) 是一種最佳化策略，在 copy-on-write 策略中如果有多個呼叫者同時要求相同資源（如記憶體或磁碟上的資料儲存），則他們會共同取得相同的指標指向相同的資源，直到某個呼叫者試圖修改資源的內容時，系統才會真正複製一份專用副本給該呼叫者，而其他呼叫者所見到的最初的資源仍然保持不變。
* Def : parent 生出 child process 後，Initially，child 先與 parent 共享 parent 的 Frame (Memory) 空間，即先不配置 frames 給 child
* 優點
  * [1] 對 Memory(frame) 需求量大幅減少
* 只有 child process 改變 page 內容時才配新 frame (copy)，如此一來可以增加建立 process 的效率，因為大部分情況 child process 並不會對資料段寫入。
* parent process 會和 child process concurrent 執行，要達到這種效果，CPU 要有 MMU 硬體支援。
[型三] vfork() 不使用 copy-on-write ( virtual memory fork() )
* parent 和 child process 除了 stack 其他都共享。 
* 不使用 copy-on-write，在出生 child 後，child 立刻執行 execlp()，去做其他 task 時適用
* vfork() 通常用於沒有 MMU 的 OS，此時 parent process 會被暫停，直到 child process執行了 exec() 或 exit()。
![image](https://user-images.githubusercontent.com/38349902/46915120-730ec200-cfd9-11e8-966b-dcefc8f83955.png)
## 重點三
### Effective Access Time (EAT) in Vertual Memory & Page fault ratio 
* EAT = (1 – p) x memory access time + p x (page fault overhead + swap page out + swap page in + restart overhead)
  * p :  Page fault ratio 
* 欲提高 Virtual Memory 之效率，就是要降低 effective memory access time ( 降低 page fault ratio )
* 影響 Page fault ratio 之因素
  * [1] [Page Replacement algo 之選擇](#重點五)
  * [2] [Frame 數分配多寡之影響](#重點七) 
  * [3] [Page size 之影響](#重點十)
  * [4] [Program Structure 之影響](#重點十一)

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

#### ＬＲＵ與ＭＦＵ
* Def : 以 Page 的累計參考次數最為挑 Victim Page 的依據，分為兩種
  * [1] LFU : Least Frequently Used (參考次數最小者)
  * [2] MFU : Most Frequently Used (參考次數最多者)
    * 若有多個　Page 具相同值，以 FIFO 為準
* 分析
  * [1] 效果不好，page fault ratio 很高
  * [2] 遭遇 Belady Anomaly
  * [3] 製作成本高
  
#### Ｐｒｅｐａｇｉｎｇ
* Def : Prepaging 會事先猜測程式執行之初會使用哪些 page，並預先將這些 pages 載入。
* 優點：若猜得準確，則可以避免程式執行之初大量 Page Fault 發生。 
* 缺點：若猜測錯誤，則先前載入 page 的 I/O 動作白白浪費。
* 而 pure demand paging 程式執行之初不預先載入任何 page，執行之初會產生大量的 page fault，由於載入的 page 皆是 process 所需的頁面，故後續的 page   fault ratio 會下降至合理值 (趨於穩定)。


#### 比較表       
|                  |  FIFO  |    OPT   |     LRU    | LRU&MFU |
|:----------------:|:------:|:--------:|:----------:|:-------:|
| Page fault ratio | 相當高 |   最低   |  尚可接受  |   很高  |
|       效果       |  不好  |   最佳   |    還行    |   不好  |
|  Stack Property  |    ×   |     √    |      √     |    ×    |
|  Belady Anomaly  |    √   |     ×    |      ×     |    √    |
|       NOTE       |        | 無法實作 | 硬體成本高 |  成本高 |

## 重點六
### Page Buffering 機制
* 緣由 : Page fault 發生且要做 Page Replacement，且 the victim page has been modified，then
  * (1) 要先把 victim page 寫回 disk
  * (2) 才載入 missed page
  * (3) process can resume execution
    * 上述流程，會導致 process restart later
    * 希望可以加速，讓 process restart exec. as soon as possible
* 解法 
一般 O.S 分配給 process 之 pages(frame) 稱 " resident pages "
  * [法一] O.S 會建一個 " Free Frame Pool " 保有一些 Frames，這些 Frames 並不是拿來配置給 Process 用，而是用來當 O.S 的周轉金(私房錢)
    ![image](https://user-images.githubusercontent.com/38349902/46913490-5fa32d00-cfc0-11e8-8bbe-870d28012795.png)
  * [法二] O.S 會保存一條串列 " Modifued Pages List "，紀錄 Modification 
    * Bit 為 1 之 Pages，只要 I/O 設備一有空，O.S 就將此串列中的一些 modified pages 寫回 disk，然後 reset modification bit 為 0，如此一來未來挑       victim page 時，挑出的 page 是 unmodified 的機會大增，因此也可縮短 process restart exec. time
  * [法三] 把[法一]進一步改良
    * 針對 free frame pool 中所有的 free frame，紀錄是哪個 process 的哪個 page 放在 free frame 中，因為他們均是 " the recent updated content "，而流程修改如下
![image](https://user-images.githubusercontent.com/38349902/46913571-6e8adf00-cfc2-11e8-9c60-9383df362da5.png)

## 重點七
### Free Frame 分配多寡對 page fault ratio 之影響
* 一般來說，Process所分配到的 frame 愈多，則 page fault ratio 愈低。 
* O.S 在分配 Process Frame 時，數目有最少數目與最大數目的限制，此兩類數目限制均取決於 Hardware 因素。
   * 最大數目的限制：由 physical memory size 決定 
   * 最少數目的限制：由機器指令結構決定，必須能讓任何一個機器指令順利執行完成，即機器指令執行過程中，Memory Access可能之最多次數。  
  　  *  e.g. IF - ID - EX - MEM - WB 中，IF 必有記憶體存取，MEM、WB可能有必有記憶體存取，因此最少的 frame 數目為 3。

## 重點八
### Thrashing
* Thrashing 現象：
  * 若 Process 分配到的 frame 不足，則會經常發生 page fault，此時必須執行 page replacement。
  * 若採用 global replacement policy，則此 process 會去搶其它 process 的 frame，造成其它 process 也會發生 page fault，而這些 process 也會去搶     其它 process 的 frame，造成所有 process 皆在處理 page fault。
  * 所有 process 皆忙於 swap in/out，造成 CPU idle。
  * CPU idle 時 OS 會企圖調高 Multiprogramming Degree，但因為 frame 樹本來就不足，引進更多的 process 進入系統讓 Thrashing 現象更嚴重。
  * 結果 
    * [1] CPU utilization 急速下降
    * [2] I/O-Device 異常忙碌
    * [3] Process 花在 Page fault process time >> 正常 exec. time  
![image](https://user-images.githubusercontent.com/38349902/46914020-65057500-cfca-11e8-8496-6837965722ae.png)
* Thrashing的解決方法：
  * [法一] 降低 Multiprogramming degree
  * [法二] 利用 Page Fault Frequency Control 機制來防止 thrashing
    * 作法 : OS 規定合理的 page fault ratio 之上限與下限值，把 ratio 控制在一個合理範圍內。 
      * case 1 : page fault ratio > 上限值 → OS 應多分配額外的 frame 給該 process。 
      * case 2 : page fault ratio < 下限值 → OS 應從該 process 取走多餘的 frame，以分配給其它有需要的 process。  
![image](https://user-images.githubusercontent.com/38349902/46914097-8d41a380-cfcb-11e8-91f5-726c28fe4cbc.png)
  * [法三] 利用 [Working Set Model](#重點九)預估各 process 在不同執行時期所需的 frame 數目，並依此提供足夠的 frame 以防止thrashing。
  
## 重點九
### Working Set Model
預估各 process 在不同執行時期所需的 frame 數目，並依此提供足夠的 frame 以防止thrashing。
* (一) 架構在 " Locality Model " 理論上
  * Def : process 執行時對於 memory 的存取區域具"集中/局部(locality)"性質，並非是均勻的，分為兩種。
    * Temporal locality：Process目前所存取的記憶體區域過不久後會再度被存取，e.g. loop, subroutine(副程式), counter, stack(Top). 
    * Spatial locality：process 目前所存取的記憶體區域其鄰近區域極有可能也會被存取，e.g. array, sequential code execution, global data area ,                         linear search , vector operation
* (二) 符合 locality 者，Page fault ratio 相對較低
  * program 中若使用皆符合 locality 之 Data structure、指令、algo者，則為 Good，反之為 Bad
* (三) 相關名詞
  * Working set window : 記為△
    * 代表以 △ 次 memory reference 作為統計 working set 之依據
  * Working set : 在 △ 次 pages 存取中所參考到的不同 Pages No. 之集合
  * Working set Size (WSS) : Working set 中之元素(Page)個數，即 Process 在此時期所需之 frame 數  
  ![image](https://user-images.githubusercontent.com/38349902/46914465-adc02c80-cfd0-11e8-9195-7555b5a7616b.png)
* (四) O.S 如何運用?
  * 假設有 n 個 processes，令
    * WSSi : process i 在某時期的 working set size。 
    * D : 所有 process 的 WSS 總和 = 所有 process 之 frame 總需求量。
    * M : physical memory frame 總數
  * 分為兩個 Cases 
    * Case 1 : D ≤ M → OS 會依據 WSSi，分配足夠的 frame 給 process，可防止 thrashing。
    * Case 2 : D > M → OS 會選擇 process swap out，直到 D ≤ M 為止，此時回到 Case 1 處理，等到未來 frame 足夠時再恢復原先swap out 的 process。
* (五) 優 & 缺 
  * 優點
    * [1] 可以防止 thrashing 產生，對於 prepaging 亦有幫助。 
  * 缺點
    * [1] 以上一次的 working set 來預估下一次的 working set，不易制定精確的 working set。
    * [2] 若前後的 working set內容差異太大，則 I/O transfer time 會拉長。
    
## 重點十
### Page Size 對 Page Fault Ratio 之影響 (TLB reach)
page(frame) size 越小則
* 優點
  * [1] 內部碎裂輕微
  * [2] 單一 page 的 transfer Time 越短
  * [3] locality 越集(好)
* 缺點
  * [1] page fault ratio 愈高
  * [2] page table size 愈大 (∵ entry size 固定 )
  * [3] 執行整個 process 的 I/O 時間越長
* 目前趨勢傾向於設計大的 page size。
* TLB reach
  * Def : 經由 TLB 所能存取到的 Memory area 大小
    * 公式 : TLB reach = TLB entry × Frame size
  * 如何加大 TLB reach?
    * [法一] 加大 TLB entry
      * 缺點 
        * [1] 成本(價格)貴，且耗能
        * [2] 擴充程度有限，不足以涵蓋 process 之 working set
    * [法二] 加大 Page size
      * 缺點
        * [1] 內碎較嚴重
          * 解法 : 現代 Hardware 可以提供多種不同的 Page size，由 O.S 負責管理 TLB entry
## 重點十一
#### Page Structure 對 Page Fault Ratio 之影響
所使用的資料結構與演算法是不是好的。判斷好或不好，在於符不符合 locality。
* Good : Loop, Subroutine, Counter, Stack, Array, Sequential Code Execution, Global Data Area, Sequential Search. 
* Bad : Link list, Hashing Binary Search, goto, jump.
* array 的相關處理程式，最好與 array 在記憶體中的儲存方式一致 (即 Row-major, Column-major 相關概念)。 

## 重點十二
#### 記憶體映射檔案 Memory-Mapped Files
記憶體映射檔案 (Memory-Mapped Files) 會讓一段虛擬記憶體對應於一個檔案，將檔案 I/O 視為經常性的記憶體，而非一般的 open(), read(), write() 標準系統存取。這讓程式設計師可以藉由撰寫記憶體映射檔案相關的函式庫程式 (如 c 的 mmap, java 的 java.nio package) 直接從虛擬記憶體讀取檔案內容，達到加速 I/O 效能的目的，整理其特性如下：
* 高速檔案存取 (主要目的)
  * 比直接讀寫檔案快幾個數量級
  * 傳統檔案每次被讀取時需要一次系統呼叫和一次磁碟存取
* 將大檔案載入到記憶體。
  * 導致內部碎片空間浪費 (對齊頁，通常為 4 KB)。
  * 對映檔案區域的能力取決於於記憶體定址的大小：在 32 bit 機器中，不能超過 4GB。
  * 可能導致頁面錯誤的數目增加
* 可讓多個程式共享記憶體，直接對記憶體進行讀取和寫入來修改檔案。 
* 使程式動態載入
  * 為 Linux dynamic loading 實作方式。
  Linux 提供了記憶體映射檔案的函數 mmap，它把檔案內容映射到一段虛擬記憶體
  ![image](https://user-images.githubusercontent.com/38349902/46915067-cb918f80-cfd8-11e8-92ee-e2a3f72c3802.png)
 
  
### References
http://mropengate.blogspot.com/2015/01/operating-system-ch9-virtual-memory.html

       
