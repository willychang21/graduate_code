# Ch6 Memory
* [重點一、Principle of Locality](#重點一)
* [重點二、Memory Hierarchy](#重點二)


## 重點一
### Pinciple of Locality
* Pinciple of locality  
程式在任何時間點只會存取一小部分的位址空間，稱為區域性(Locality)
  * [1] **temporal locality** : 一個 item(instruction or data) 被存取到，很有可能會被再存取到。(e.g. loop)
  * [2] **spatial locality** : 一個 item 被存取到，它位址附近的 item 也很快會被存取到。(e.g. array,program)
* Memory Technology
  * **SRAM** (static random access memory)
    * use for **cache**
    * static: content will last forever until lost power
    * low density, high power, fast
  * **DRAM** (dynamic random access memory)
    * use for **main memory**
    * dynamic: need to be fresh regularly
    * high density, low power, slow
  * Magnetic disk

## 重點二
### Memory Hierarchy
![image](https://user-images.githubusercontent.com/38349902/47250534-43d2d780-d455-11e8-8327-587aacc0e3e5.png)
* at any given time, data is copied between only two adjacent levels
  * upper level: 離 Processor(CPU) 越近，記憶體越快 e.g. Cache 
  * lower level: 離 Processor(CPU) 越遠，記憶體越慢 e.g. Disk 
* 目的 : 用便宜的技術擁有足夠的記憶體，並用最快的記憶體提供最快的存取速度
* **Block** : the basic unit of information transfer
* terminology (術語)
  * hit: data appears in upper level
    * hit rate: fraction of memory access found in the upper level
    * hit time: 判斷記憶體是否hit + 把上層資料搬到處理器的時間
  * miss: data needs to be retrived from a block in the lower level
    * miss rate: 1 - (hit rate)
    * miss penalty: Time to replace a block in the upper level(主要) + time to deliver the block to processor(CPU)裡的 cache (L1 Cache)
  * hit time << miss penalty
## 重點三
### Cache
#### direct-mapped cache
![image](https://user-images.githubusercontent.com/38349902/47252361-f7e45a80-d475-11e8-9ba3-47a5b45a614a.png)
* 在 cache 裡要儲存 valid bit + tag + data
  * valid bit
    * 紀錄 cache 內是否為有效資訊
    * 1 = present, 0 = not present
    * initially 0，因為處理器剛啟動時，內容全部是無效的  
![image](https://user-images.githubusercontent.com/38349902/47253036-6e865580-d480-11e8-917b-05aa0485b4d8.png)
#### Spatial locality 優點
* 因為有 spatial locality，所以希望一次可以搬好幾個 word 進來，也就是希望 block size 要大一點，讓這些 word 短時間內存取機率高
* 16 個 word 共用 1 個 tag & valid bit，且 tag 的共用提高快取內記憶體使用的效率 
![image](https://user-images.githubusercontent.com/38349902/47253146-08023700-d482-11e8-8bf7-60ee96c20f86.png)
#### block size & missed rate 
![image](https://user-images.githubusercontent.com/38349902/47253426-eb1b3300-d484-11e8-958a-9d2b59ae62de.png)
Q : Block size ↑ , Miss rate 卻是先下降再上升, why??
A : 一開始沒掉下來的原因是 Block size 太小，沒有得到spatial locality 的好處，再放大才能得之好處，但太大，整個 cache space 是固定的，當 cache block 總數 ↓，block & block 會互相競爭，因此 Miss rate ↑，miss penalty ↑
* 減少 miss penality 方法
  * [法一] early restart : 送到需要的 word 時就直接開始執行，而不是等整個 block 送過來才開始
  * [法二] request(critical) word first : 需要的 word 會先從 memory 送到 Cache 中，剩下的 word 才會被 load
  * request(critical) word first 比較快

## 重點四
### Cache Concept
#### Cache miss handling
* Cache miss handling 基本方法
  * stall the CPU，凍結 register 內容值
  * use Cache Controller to handle Cache , load the miss block from memory to cache
  * 當資料 load 進 Cache 後，重新執行 Cache miss 的那個 Cycle
* Multi-Cycle or Pipeline handling insCache miss Steps
  * (1) 將原始 PC (目前 PC - 4)送到 Memory
  * (2) 通知 Main Memory 執行讀取，並等待 Memory 完成此存取動作
  * (3) 寫入 Cache，將從 Memory 得到的 Data 放入適當的欄位，將位址的上半部分(來自CPU)寫入 Tag 欄位，並 Set Valid bit
  * (4) 重新啟動上述 Step 1 的 instruction，refetch 已經由 Memory 送至 Cache 的 instruction
* Cache 對資料存取的控制在本質上是相同的，一旦失誤僅處理 CPU 直到 Memory 將 Data 回應
#### Cache Write-in handling
* 假設使用 sw 將 data 只有寫入 Cahce，沒改變 Main Memory 的值 → inconsistent (不一致)
* 解決方法 
  * [1] write-though : 寫入 Cache，也寫入 Main Memory
    * Simple but bad performance
    * 解決 : 使用 write buffer 
      * write buffer 儲存等待被寫入 Memory 的 Data。
        * FIFO
        * typical number of entries: 4
        * write buffer 沒滿 : processor 繼續執行
        * wirte buffer 滿了 : processor stall until write buffer 有空位
    * use No Write Allocate(Write around) (p26.)
  * [2] write-back : 新的 data 只會被寫入 Cache。當備置換時，修改過(dirty)的 block 才會被寫回 lower level memory
    * improve performance but hard to implement
    * can use a write buffer to allow replacing block to be read first
    * use Write Allocate (p26.)
#### Write Allocate
* 發生 Write Miss 處理方法
  * [1] Write Allocate : 從 Memory 搬所需 Block 到 Cache 再寫入
  * [2] No Write Allocate(Write around) : 所需 Block 只搬到 Memory，不搬到 Cache，且 Reset Cache block's valid bit 為 0 (not fresh)
    * 想法 : 程式寫入整個 Block，初次 Write Miss 帶來的 Block 是不需要的
#### Split Cache & Combined Cache 
* use two same capacity split cache's combined cache
* high hit rate
* split to instruction Cache & data Cache → bandwidth ↑
* bandwidth 變兩倍的好處，輕易克服 Miss rate 稍微增加所帶來的壞處
#### Memory Design to Support Cache



  
  




















