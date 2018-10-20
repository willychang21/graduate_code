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
    * miss penalty: time to replace a block in the upper level (主要) + time to deliver the block to the processor
  * hit time << miss penalty
## 重點三
### Cache
#### direct-mapped cache




















