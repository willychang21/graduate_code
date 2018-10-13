# Memory Management
* [Binding](#Binding)

## Binding
* **Binding** : 決定程式在記憶體執行之起始位址
![image](https://user-images.githubusercontent.com/38349902/46900926-04e4d500-cedd-11e8-9e0b-98e0afa2aaeb.png)
* **Stactic Binding** ( logical address = physical address )
   * **Compiling Time** (Compiler) → 如果編譯時，程式所在的記憶體位置已知，那麼可產生absolute code，將來程式執行的起始位址不得變更 ； 如果起始位置變化，必須重新編譯代碼。
       * 缺點：若所決定的位址內有其它的程式在執行，或之後要變更程式執行的起始位址，則須 recompile。
        
   * **Loading Time** (linking loader) → 如果編譯時不能確定程式所在的記憶體位置，則必須生成relocatable code
     * 缺點 
       * execution time 沒有被呼叫到的模組仍需事先 linking, Allocation, Loading，浪費時間也浪費記憶體。 (e.g. if-else 的程序、OS 錯誤處理程序。)
       * 程式執行期間仍不可以改變起始位址。
       ![image](https://user-images.githubusercontent.com/38349902/46678039-5576dc80-cc16-11e8-9190-252398db7a76.png)  
* **Dynamic Binding** ( logical address ≠ physical address ) 
  * **Execution Time** (O.S) → 在程式執行期間才決定程式執行的起始位址。需要的額外硬體支援(Memory-Management Unit, MMU)。
    * 優點
      * [1]：Process在執行期間可以任意更動其起始位址，仍能正確執行
      * [2]：有利於O.S彈性地管理Memory Space之配置(彈性高)
    * 缺點
      * [1]：需要Hardware額外支持 → 不是O.S(∵要用O.S需interrupt，而此操作太頻繁不適合)
      * [2]：Process Exec.Time 變長 ，Performance 較差
    * Paging是用Dynamic binding，雖然Paging也可以用Compiling Time 或 linking Time，但沒什麼意義
* Logical address : CPU → Logical address (Virtual addr.) → Logical address space      
  Physical address : Memory → Physical address (Hareware site e.g RAM) → Physical address space
  ![image](https://user-images.githubusercontent.com/38349902/46900901-84be6f80-cedc-11e8-8b2e-c385f62f8e1a.png)
* **Static link** → Compile 時 library 就加入程式碼(好幾個程式都#include <stdio.h>，就會有好幾份程式碼分布在記憶體當中，很冗)
* **Dynamic linking** → call module 後 ，load + link(好幾個程式都#include <stdio.h>，只load一份，大家共用(Share library) 。
    * **Def** : module 間的 External symbol reference (外部符號參考)之工作，延到 execution time 才做，即不事先
　　　          linking，而是在 execution time 時等某個 module 被 call 時，才 load 進 main memory 中並與其他 modules 
    　　        進行 linking 修正  
    **e.g.** 程式內指向 Lib 的部分稱為 stub，當作出對 Library 的要求，會檢查這個 Library 有沒有被 loaded 在 memory 中，如果沒有就 load
    * 優點
      * [1]：節省不必要的 linking time (e.g. library 中有100個 fucntions，但實際只需要用2個)
      * [2]：節省 main memory 空間。(即是 dynamic 方法的目的)
      * [3]：節省編譯、組譯、連結所花費的時間。(動態連結函式庫可以單獨重新編譯)
      * [4]: 常用於 library 導入
    * 缺點
      * [1]：需要 OS 支持，不同 OS 有不同的稱呼 ( ∵ 任一 process 無法 access 別的 process 的 mem space → need O.S )
        * Windows .dll (Dynamic Linking Libraries)
        * Linux .so (Shared Object)。
    * Dynamic linking 必定 Dynamic loading ， 但Dynamic loading 可以 Static linking ( 先 link 好 )
* **Dynamic loading** → call modile 後 ， only load ( no link )
    * **Def** : 即"load on call"，Compile時，不將library加入程式碼，在 Execution Time，當某個module(founction)被call，若未在memory中，將                     library載入，用完後再free出空間
    * 優點
      * [1]：節省 main memory 空間 ( 用到才 load )
      * [2]：讓 programmer 可以呼叫 loader，比 dynamic linking 更具有彈性，靈活度也更高。
      * [3]：不須 O.S 額外支持 ( ∵ only load → 只需 MMU )
    * 缺點
      * [1]：programmer 的負擔，要 programmer 自己規劃而不是 OS 負責。
      * [2]：拖長執行時間 → Performance 差 ( ∵ 停下來等 I/O )
      * [3]：dynamic loading 是古老的方法，e.g. MS-DOS Overlay files
 
 
 ## Contiguous Memory Allocation
 * **Def** : process 必須佔用連續的 memory space ， OS 依據各個 Process 的大小找到一塊夠大的連續可用的記憶體，配置給該 process 使用
 * **Partition** : process 所佔用的 memory spcae ， 由於 process 大小不一 ， 且 process 數目也多變 ， 因此每個 process size 不一定相同 ，且                      partition 數目也不固定  
                   [註] 早期也可採用固定大小，固定數目之partition
 * **Available list(AV-list)** :  OS 會利用 Link List 管理 Free Memory Blocks (Available hole)，稱為 Available list。
 * **Single-partition allocation**
     * Relocation-register scheme used to protect user processes from each other, and from changing OS code and data. 
     * Relocation register contains value of smallest physical address; limit register contains range of logical addresses. Each logical        address must be less than the limit register.
 * **找尋連續可用空間的方法** ( n = process size )
     * [1] First-Fit：從 AV list head 找，第一個 free block size >= n 就配置。
     * [2] Next-Fit：從上次配置後的下一個 Block 開始搜尋，改善 First-Fit 易在 AV-list 前端附近產生許多非常小的可用空間的問題。
     * [3] Best-Fit：找所有 free block，比 n 大且最接近 n。
       * 長期而言會剩下很大的洞跟很小的洞。
     * [4] Worst-Fit：找所有 free block，比 n 大且（size – n）值最大者。
       * 長期結果每個洞大小差不多。
     * [5] Buddy System：16,8,4,2,1的二元樹，每一層有 list 可以搜尋有無空間。
     ![image](https://user-images.githubusercontent.com/38349902/46901739-51cfa800-ceeb-11e8-8512-d6cc39e95283.png)
 * **Contiguous Allocation 缺點**
     * 均有外部碎裂(External Fragmentation)問題：所有可用空間總和大於某個 process 所需要，但因為此空間不連續所以無法配給該 process 使用，造成            memory 空間閒置。
     * 配置完所剩的極小 Free Blocks 仍會保存在 AV-list 中，徒增 Search Time 與記錄成本。 
 
 ## Fragmentation
 * **外部碎裂 (External Fragmentation)** ( 空房是夠住的，但空房但都分散開(規定要住旁邊)，造成空屋太多 )
     * **Def** : AV-list 中 all free block size sum ≧ process size，但因為這些空間不連續(in contiguous allcoation)所以無法配給該 process 使                  用，造成 memory 空間閒置。
     * **解決方法**
         * [1]： Compaction
         * [2]： Page memory management
 * **內部碎裂 (Internal Fragmentation)** ( 有空房但太大間，多的空間別人也住不進來 )
     * **Def** : O.S 配置給 process 的 memory space > process need size，多出來的空間該 process 用不到，而且也沒辦法供其他 process 使用，形成                  浪費。
     * **解決方法**
         * [1]：Reducing the page size can alleviate Internal Fragmentation.
         * [2]：Enlarging the page size helps to reduce the size of the page table. 
         
## Paging ( 算是Dynamic Binding )
OS 會將 disk 中的資料分割成固定大小的區塊，稱為頁（pages）。當不需要時，將分頁由 memory 移到 disk ；當需要時再將資料取回載入 memory 中。分頁是磁   碟和記憶體間傳輸資料塊的最小單位。
* 實體記憶體 (Physical Memory)：視為一組頁框(Frame)之集合。各頁框的大小均相等。
* 邏輯記憶體 (Logical Memory)：即 User Program 。視為一組頁面(Page)的集合。Page size = Frame size。 
* O.S 採 " 非連續性 " 配置 ，即若process大小是 n 個 Pages ，則 o.s 只要在 Physical memory 找到 n 個 free frames 即可配置，這些 frames 不一定要   連續
* O.S 會替每個 process 建立一個 Page table(分頁表) 儲存在記憶體中，紀錄 process 中每個 Page 所配置的 Frame No.，執行時用 page table 的資訊來把     logical address 轉成 physical address。
* 圖示
  ![image](https://user-images.githubusercontent.com/38349902/46902041-cf49e700-cef0-11e8-8a22-f45cea475949.png)
* logical address 轉 physical address 計算(By MMU → Hardware)
  * [1]：CPU 產生 " Logical " address (" Virtual " address)
    * 其中 p : Page No. , d : Page offset
    * Page 之 logical address 是單一量，自動拆成p,d
    * 單一量位址 ÷ Page size = 商數:p..餘數:d
  * [2]：依 p 查詢 Page table 取得該 page 之 Frame NO. : f
  * [3]：f & d 合成 physical address ( f × frame size + d )
* 優點
  * [1]：解決 external fragmentation問題 
  * [2]：可以支援Memory的共享(Sharing)：不同 page 對應相同的 frame。
    * 不同processes 可以透過各自的 Page table mapping 存取相同的 physcial address space ，可節省physical mamory space
  * [3]：可以支援Memory的保護(Protection)：在 Page table 上多加一個 protection bit 欄位
    * R : 表示Read only
    * RW : 表示Read/Write皆可 
  * [4]：支援 Dynamic Loading,Linking 及 Virtual Memory 的製作 
* 缺點
  * [1]：會有 internal fragmentation 問題 (process size 不見得是 Page size 的整數倍數，page size 愈大愈嚴重)
  * [2]：memory effective access time 較長 (logical address 轉 physical address)
  * [3]：需要額外的Hardware支援
    * Page table 製作 → 用 TLB
    * logical address 轉 physical address → MMU
    
## Page Table 的製作 (保存)
* [方法1] 使用 register 保存 Page table 每個項目(entry)的內容
  * 優點 : 存取速度快 (i.e. 查 Page table fast ∵不須 memory access) 
  * 缺點 : 僅適用於 page table 大小較小的情況，太大的 page table 則不適用。 
* [方法2] 使用 memory 保存 Page table，OS 利用 PTBR(Page Table Base Register) 記錄其在 memory 的起始位址，PTLR(Page-table length register)             紀錄 page table size 
  * 優點 : 適用於 page table size 較大之情況 
  * 缺點 : 速度慢。因為需要存取兩次 memory。(一次用於存取 page table、一次用於真正的資料存取) 
* [方法3] 使用 TLB(Transaction Lookaside Buffer register)(或叫 Association Registers) 保存部份常用的 Page table，完整的 page table 在 memory           中，TLB 是 full associate cache。  
![image](https://user-images.githubusercontent.com/38349902/46902413-bb55b380-cef7-11e8-8902-c709066efb2d.png)  
   *  TLB 的 Effective Access Time (EAT) 計算 ( P : TLB Hit ratio )
   ![image](https://user-images.githubusercontent.com/38349902/46902485-d7a62000-cef8-11e8-8c8e-11d1c79f7d95.png)
## Paging 之相關計算
* [型一] 使用 TLB 之 Effective Access Time (EAT)
* [型二] logical address & physical address 之 length ( or bits 數 )
* [型三] " Page Table " size 相關

## Structure of Page Table   
目的：page table size 太大太稀疏的解決方法。
* [方法1] Multilevel paging (多層的分頁)
  * Def : 不要一次把全部的 Page Table 都載入到 memory ，而是抓取部分(抓 1 個 Page)所需的 Page Table 內容到 Memory ，做查詢即可 ，如此可節省               Memory Space
  * 作法 : 將 Page Table 做 Paging ， 也就是做成 Mutilevel Paging

 

  
 
 

     
 
      


          
          
    

    
    
    
