## Disk system 組成
### (一)Disk Component : Sector ⇒ Track ⇒ Cylinder
### (二)描述磁碟幾何狀況 (C, H, S)
* C (cylinder) : 幾圈
* H (head) : 幾面(一片有兩面、最上最下面不算。)
* S (sector) : 一圈幾格
### (三)Disk Access Time Element
* Seek Time : Move Head to Track
  * 純機械移動 ⇒ 最耗時間
* Latency Time : Move Sector to Head
  * 又稱rotation/revolution time : rpm = rotations per minute
  * avg. latency = 1/[(rpm/60)×2]
* Transfer Time : Disk to Memory time (與傳輸量成正比)
## Disk Free Space Management 4 種方法
Disk allocation/free space 單位是以 block 為主。

### (一)Bit Vector (Bit Map)
* 定義：用一組 bits 來代表 blocks 配置與否，一個 bit 對應一個Block。
* 優點：Simple，且易於找到連續可用空間 (即找到連續的 0)。
* 缺點：bit vector 儲存佔用 memory 空間，所以此法不適用於大型 disk。
### (二)Link list 
* 定義：用 link list 將 free block 串接，形成一個 AV List。
* 優點：加入/刪除 block 容易，大型disk較適合
* 缺點 
  1.不容易找到連續的free blocks
  2.效率不佳。因為在找尋可用區塊時，需從頭檢視此串列，I/O time 過長 (檢視一個 block 就須做一次 I/O) 。
  3.Risk高(萬一list斷掉)
### (三)Combination
* 定義：link list進化版，1個block除了利用pointer指向下個free block，自己還會記錄free block no.，以方便快速找到大量free blocks
* 優點：：加入/刪除 block 容易，可迅速取得大量可用區塊。 
* 缺點：效率不佳，I/O time 過長。 
### (四)Counter
* 定義：適用於連續區塊較多之情況。在一個 free block 中，記錄其後的連續可用區塊數目 (含自已)。
* 補充：如果連續區塊數目夠多，則 link list 長度可大幅縮短。

## File Allocation 3 Methods
### (一)Contiguous Allocation
* 定義：若檔案大小為 n 個 blocks，則 OS 必須在 disk 上找到 >= n 個連續 free blocks 才能配置。 
* 記錄方式：File Name,Starting Block No,Size
* 優點
  1.平均seek time較短 (檔案所佔的連續區塊大都會落在同一個 track 上) 
  2.可支援 sequential access 及 random(direct) access 
* 缺點
  1.external fragmentation
    補充：每種 file allocation methods 均有 internal fragmentation
  2.檔案大小擴充不易(因為要連續)
  3.建檔時須事先宣告大小 (宣告要多少block)
  
### (二)Linked Allocation → FAT
* 定義：若檔案大小為 n 個區塊，則 OS 必須在 disk 上找到 >= n 個可用區塊即可配置 (不一定要連續)。各配置區塊之間以 link list 做串連。 
* 記錄方式：File Name, Start Block #, End Block # 
* 優點
  1.沒有 external fragmentation
  2.檔案可動態擴充
  3.建檔之前不用事先宣告
* 缺點
  1.不支援 random access ：要讀某個檔案的某個區塊時需從頭找起，只支援 sequential access。 
  2.squential access 速度己較慢(因為disk上reading lunk資訊，需要大量I/O) -> FAT解決
  3.seek time比 contiguous 配置方式長，非連續性 blocks 可能散落在不同的 track。
  4.Reliability 差，若連結斷裂則會資料丟失。
### FAT(File Allocation Table)Method
* 定義：是linked Allocation的變形，差別將所有配置區塊之間的 link 及 free block 資訊全部記錄在一個Table(FAT)。FAT集中在磁碟某一個固定位置，或可以將它載入主記憶體內，因此雖然在找尋指標時是循序找尋，但速度相對快很多。若 disk 有 n 個 blocks，則 FAT 有 n 個項目 (不論是Free的還是使用中的Block皆需記錄)。
* FAT中的項目資訊
  1.空白 : 表示 free block。
  2.block # : 有 link 指向此 block，即使用中的 block。
* 優點：加速R/W i'th block of a file之進行(只要在FAT table中就可以找到i'th block，不需要在disk上讀取link info
### (三)Index Allocation → UNIX の I-Node
* 定義：每個檔案皆會多配置一些額外的 blocks 作為 index blocks，內含各配置的 data block 編號。採非連續性配置方式。
* 記錄方式：File Name, Index Block # 
* 優點
  1.沒有 external fragmentation：linked list 的優點
  2.支援 random access 及 sequential access：contiguous 的優點
  3.可以動態增加大小
  4.建檔時無需事先宣告大小 
* 缺點
  1.index blocks 佔用額外空間：索引區塊佔用 free block 的空間。
  2.一般而言，Indexed Allocation 浪費的 linking space 多於 linked Allocation
  3.index block 太小不夠存放一個檔案的所有 block 編號，太大又形成浪費(此問題必須解決)。
### 解決 index block 不夠大的三個方法
* [法一]使用多個Index Blocks且彼此用linking方式串聯
  * 缺點
    1.存取i'th block I/O次數線性成長
    2.喪失 "Random Access"意義
* [法二]階層式之Index Structure
  * 優點
    1.適用大檔案
    2.平均 I/O 次數 for access i'th block 是相同的 (較少)
  * 缺點：小檔案不適合，index blocks 很浪費時間
  
* [法三]混和方法UNIX的I-Node Structure
  ```C
  if(file.size < 12 blocks)
    用前12格blcoks就好
  else if(file.size > 12 blocks)
    用前12格 + n + n^2 + n^3 個 blcoks 
  ```
  * 優點：大小型皆適用
 ![](https://i.imgur.com/TeML9xu.png)

## Disk Sceduling Algo
Disk Sceduling 沒有最好跟最差
  * FCFS
  * SSTF
  * SCAN
  * C-SCAN
  * LOOK
  * C-LOOK
### (一) FCFS 演算法 (first-come, first-serve)
* 定義：先到達的請求優先服務。
* 分析
  1.容易製作
  2.公平，no starvation
  3.排班效果不好，即平均 seek time 長
  
### (二) SSTF 演算法 (short-seek time first)
* 定義：距離目前 R/W Head 最近的 track 優先服務。
* 分析
  1.排班效果不錯，但並非是最佳的 (平均移動的 track 總數量無法保証為最少)。
  2.不公平，可能會發生 starvation。 

### (三) SCAN 演算法
* 定義：讀寫頭來回雙向移動，不停地掃描，若有 track 請求則服務，當磁頭遇到 track 開端或尾端才會折返。
* 分析
  1.排班效果尚可，適用大量負載(disk request 大量，heavy loading)的情況(因為可提供較均勻 uniform 的等待時間)(Look/C-Look/C-SCAN也是)
  2.在某些時間點，對某些 track request，似乎不盡公平。→ C-SCAN 解決
  3.磁碟必須碰到 track 開端或盡頭才折返，此舉浪費不必要的 seek time → Look 解決

### (四) C-SCAN 演算法
* 定義：SCAN變形，只提供單向服務
* 爭議：計算拉回的 track 數要 列入/不列入 計算

### (五) LOOK 演算法
* 定義：SCAN變形，不同在於處理完某方向的最後一個 track 請求即折返，不需遇到 track 起頭與尾端才折返。
### (六) C-LOOK 演算法
* 定義：Look變形，只提供單向服務

### disk scheduling algorithm 常識
* Either SSTF or LOOK is a reasonable choice for the default algorithm. 
* Disk scheduling problem is inherently NP-hard. There is no optimal solution! Heuristics may be taken. 
* Modern disks impose nearly the same overheads on seek and rotation.

## 名詞解釋
### (一) Formating格式化
* Physical(Low-level formatting)
  * Dividing a disk into sectors that the disk controller can read and write. 
  * Remapping bad tracks
  * Zoned-bit encoding
  * 原廠或完整格式化時使用 (防老化)。
* Logical
  * user 開始使用 disk 前，O.S 必須作 2 個工作
    1.Partition 劃分：O.S 視每個 Partition 為一個分離的 Disk
    2.Logical format：O.S 要建立 File management system 所需的 data structure , physical directory , etc 並存於 disk。
    3.快速格式化，通常使用者格式化為此類，只寫掉檔案系統要用的表格。

### (二) Raw-I/O (原始的 I/O)
* 定義：將 disk 視為一個大型 array 用，沒有任何 File system 支援
* 優點：速度快 (e.g. DBMS之內層) 

### (三) Boot block initializes system
* The bootstrap is stored in ROM.
* Bootstrap loader program
  * 傳統：bootstrap loader (ROM)，找 OS object code 載入 memory。
  * 現在：simple bootstrap (ROM) => 完整 bootstrap (Disk) => OS object code (Disk) => 開機完成。
  * 完整的 bootstrap loader 是存放在 Disk 中固定位置，稱為 boot blocks。
  * 擁有 boot blocks 之 disk 稱為 boot disk 或 system disk。
  ![](https://i.imgur.com/cjtV6cn.png)
 
### (四) Bad Sectors 之處理方法
Sector 在工廠製造時或是正常使用一段時間後可能會損毀
* Mark Bad Sector 告訴 O.S 以後不用
* Sector Sparing (備料)
  * sector 分為 2 大部分
    * 正常 sector 可管理/配置的 sector
    * spare sector
      1.low-level formatting 完成
      2.O.S 看不到、O.S 無法管理
      3.供 SCSI Disk Controller 管理
  * 功能：SCSI 轉向將 spare sector 代替 bad sector 並告訴 O.S 已修復。 
  * 缺點：SCSI 的轉向動作，可能會破壞掉 Disk scheduling 的效能。
  * 解此缺點：會將這些 spare sector 散落在同一條/鄰近的 track 上，不要集中。 
* Sector Slipping
  * 功能：避免 spare sector 的轉向破壞 disk scheduling performance。
### (五) Swap Space Management
* Disk 中用來儲存 swap out 的 process 或 page 的空間 (from virtual memory or medium term scheduler)
* 一般而言，預留的 swap space 大一點比較好，不要太小，以增加安全性。
* 如何管理存放在 swap space 中的資料？
  * [法一] 利用 file system 管理
    * 優點：容易實施、製作
    * 缺點
      1.比較耗時，performance 差 (因為需要找配置空間，在 physical directory 搜尋/紀錄)
      2.若採 Contiguous Allocation，則可能有外碎
  * [法二] 利用獨立的"Partition"來保存，使用 RAW-I/O，無 File system support
    * 優點：速度快 (accessing or swapping)
    * 缺點
      1.有內碎
      2.萬一 partition size 不夠，則需要重新 partition
            
### (六) Disk performance 的改善/提升方法
* Data Striping
  * 定義：利用多部 physical disks 作為單一的 logical disk 使用，運用 data 平行寫入/讀取方式，提升 disk access performance
  * 分為兩種
    1.bit-level striping
    2.block-level striping
  * 注意：只有提高 performance，但不能提高 reliability

### (七) Disk Reliability 的提升方法
當 Disk 某些 Block BAD，則 Data 如何 Recovery？
* [法一] 使用 mirror (or shadowing)
  * 定義：每一部正常(作業中)的 disk，均會有一個對應的"mirror disk"
  * 功能：萬一作業中的 disk BAD，則直接用 its mirror disk 取代。
  * 優點
    1.可靠度最高
    2.data recovery 速度最快
  * 缺點：cost 相當高
* [法二]利用 parity check
  * 優點：cost 較 mirror 低
  * 缺點
    1.data recovery 速度慢
    2.data 寫入也較慢 (要算出parity bit)
    3.可靠度較低 (若多個 blocks 同時 bad 就無法 recovery)

## RAID(Redundant Array of Independent Disks) 
簡稱磁碟陣列，由多顆磁碟機組成一個陣列，將資料以分段 (striping) 的方式同時對不同的磁碟做讀寫的動作。 使用 RAID 有以下目的：
* 提升可靠度：provides reliability via redundancy. 
* 增進效能：improving performance by means of parallelism.

RAID 把多個硬碟組合成為一個邏輯磁區，因此 OS 只會把它當作一個硬碟。RAID Level 高低不完全代表系統效能或可靠度的高低，端視管理員的需求。

### RAID 0：Striping/Span
將資料切割存放到多部硬碟中且不會重複存放。
* 優點：效能改善，可進行平行讀寫，適用於具有高效能需求的系統。理論上，硬碟越多效能就等於[單一硬碟效能]×[硬碟數]。事實上受限於匯流排I/O瓶頸及其它硬體因素的影響，RAID 效能會隨邊際遞減。 
* 缺點：完全沒有容錯能力

### RAID 1：Mirroring

必須由 2 個以上的硬碟所組成，有資料寫入時，直接同時寫入兩個硬碟，故內部資料是完全一樣的。

* 優點：系統可靠度很高，極佳的資料安全性，實作容易。
* 缺點：效率最差，且需花費兩倍的成本。

### RAID 0+1：Mirror + Striping

結合了 RAID 0 與 RAID 1 兩種模式。至少要 4 顆以上的磁碟，且硬碟總數需為偶數。

* 優點：擁有 RAID 1 容錯力，以及 RAID 0 整體讀寫速度。 
* 缺點：一次最少要用上 4 顆硬碟，且也要浪費一半的總容量，成本很高。


### RAID 2：Hamming Code [無實質產品]

加入了錯誤修正碼 (ECC, Error Correction Code)，不同的位元儲存在不同的硬碟中，具錯誤檢查與更正能力，當一部硬碟壞掉時，可由其它硬碟的內容來更正。例如：4 個 Bits 的資料需要 7 部硬碟來儲存，4 部存資料，3 部存Hamming Code。
* 缺點
  1.成本下降有限(只比 mirror 少一部)
  2.可靠度與 RAID 3 相同，但成本比 RAID 3 高


### RAID 3：Parallel with Parity

多利用一顆磁碟儲存同位元資訊達到容錯功能。當硬碟有問題，可由同位元檢測得知。所需的硬碟數較 RAID 2 節省。不是個好方法，因為全部的 disk 都不斷被寫 (Bit－interleaving)，效能低落。

[感覺] 同位元 Ap = A1  XOR  A2  XOR  A3，同位元 (parity) 是效能瓶頸所在。


### RAID 4：Parallel with Parity (block)

跟 RAID 3 相同，不過其支援的資料交插是以 block 為單位計算的。P-disk 不斷被寫，效能瓶頸。


### RAID 5：Striping with Rotating Parity

RAID 5 是一種儲存效能、資料安全和儲存成本兼顧的儲存解決方案，有較多的商業應用。與 RAID 4 雷同，但是 RAID 5 把同位元`分散依序`儲存於陣列中的每顆磁碟內，改善了 P-disk 不斷被寫的效能瓶頸。


### RAID 6：Read Solomon Code [無實質產品]

RAID 6 增加第 2 個獨立的奇偶校驗資訊塊。容許 2 個硬碟同時損壞，至少必須具備 4 或 4 個以上硬碟才能生效。RAID 6 在硬體磁碟陣列卡的功能中，也是最常見的磁碟陣列等級。

筆記：採取 P + Q 錯誤偵測/更正技術，可防止多部 Disk (2 部)同時 BAD，但 Cost 極度昂貴 (more than mirror)
![](https://i.imgur.com/UguAjgf.png)


## File Mangement (看筆記)
### (一) File open/close
* 緣由：要對 file 進行人和運作前，O.S需先到 Disk 的 physical directory 搜尋已取得 file 的 allocation info，才可以對 file 進行運作
  * 缺點
    1.search time 耗時 (因為 physical directory 中的 file 數目眾多)
    2.過多 I/O 運作 (因為經常去 disk access physical directory)
### (二)Consistency Semantic
* UNIX Semantic
* Session Semantic
* Immutable Semantic

### (三) File Protection 
* Physical
* Logical
      
        
  
      
  
