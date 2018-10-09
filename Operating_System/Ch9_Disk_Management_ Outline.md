* **Disk Component** : Sector ⇒ Track ⇒ Cylinder
* **Disk Access Time Element**
    * Seek Time : Move Head to Track
    * Latency Time : Move Sector to Head
      * rotation/revolution time : rpm = rotations per minute
      * avg. latency 
    * Transfer Time : Disk to Memory
* **Disk Free Space Management Method**
    * Bit Vector (Bit Map)
    * Link list 
    * Combine (Grouping)
    * Counter
* **File Allocation Method**
    * Contiguous Allocation
    * Linked Allocation → FAT
    * Index Allocation → UNIX の I-Node
      * 解決單一index Block不夠大之方法
        * [法一]使用多個Index Blocks且彼此用linking方式串聯
        * [法二]階層式之Index Structure
        * [法三]混和方法UNIX的I-Node Structure
* **Disk Sceduling Algo**
    * FCFS
    * SSTF
    * SCAN
    * C-SCAN
    * LOOK
    * C-LOOK
* **名詞解釋**
    * Formating
      * Physical
      * Logical
    * Raw-I/O
    * Bootstrap loader
    * Bad Sectors 之處理方法
      * Mark Bad Sector以後不用
      * Sector Sparing
      * Sector Slipping
    * Swap Space Management
      * File System
      * Independent Partition
    * Disk Performance
    * Disk Reliability提升方法
      * mirror
      * parity check
    * RAID 1~6
* **File Mangement**
    * File open/close
    * Consistency Semantic
      * UNIX Semantic
      * Session Semantic
      * Immutable Semantic
    * File Access Control
    * File Protection 
      * Physical
      * Logical
      
        
  
      
  
