### 代數系統
* **Binary Operation**  
  \* : A × B → C founction  
  i.e. ∀a∈A,b∈B,∃!c∈C ∋*(a,b)=c ,記`a*b=C`  
       稱 \* =binary operation from A×B to C  
       當A×B=C=S, 稱 \* 為`bianry operation on s`
* **Algebraic System (AS)**  
  \*1,.....,\*k 為S上之binary operation  
  稱(S,\*1,.....,\*k) 為algebraic system
  * 表示法 ： 二元運算表
  (S,*)=AS,S={a1,....an}
* **close(Closure)**　: 若∀a,b∈S ⇒ `a*b∈S` , 則稱(S,\*)具closed    
  **associative**　　: 若∀a,b∈S ⇒ `(a*b)*c=a*(b*c)`,則稱(S,\*)具associative 
    * **commutative**　: 若∀a,b∈S ⇒ `a*b=b*a` , 則稱(S,\*)具commutative
* **left identity** : 若∃el ∈S ∋ ∀a∈S,`el*a=a` ,稱el為(S,\*)之left identity    
  **right idntity** : 若∃er ∈S ∋ ∀a∈S,`a*er=a` ,稱er為(S,\*)之right identity    
  **identity**　　: 若∃ e ∈S ∋ ∀a∈S,`a*e=a=e*a` ,稱e為(S,\*)之identity
  * (S,*):AS,具左單el,右單er,則el=er(∵單位元素存在必唯一)
* **left inverse**  : 若∃bl∈S ∋`bl*a=e`,稱bl為a之left inverse   
  **right inverse** : 若∃br∈S ∋`a*br=e`,稱br為a之right inverse  
  **inverse**  　 　: 若∃b ∈S ∋`a*b=e=b*a`,稱b為a之inverse(個別元素之inverse)
  * **inverse property** : 若∀a∈S,a之inverse存在,稱(S,\*)具inverse property (系統的inverse)

### 群

| 名稱 | closed | associative | identity | inverse property |commutative |
| :--: |:----: |:---------: |:----------: |:--------: |:---------------: |
|半群(semigroup)|√     |√             |          |                  |     |
|單群(monoid)   |√      |√             |√          |                  |    |
|群(group)      |√       |√             | √         |√                  |   |
|交換群(abelian group) |√             |√             |√          |√                  |√   |

* (G,*):group , a,b ∈ G 
  * G 之 idenitity `存在唯一` ,記做e
  * ∀a∈G,a之inverse`存在唯一` ,記做a^(-1)
  * (a^-1)^-1 = a 
  * ab^-1 = (b^-1)(a^-1) (∵沒有commutative)
  * a^2 = e ⇒ G is an abelian group
    * (S,\*):AS具**結合性** 
       a^2=a\*a  
       a^3=a\*a\*a  
       a^k=a\*...\*a  
       a^0=e → a^(-k)=(a^k)^-1
    * (Z,+)在加法群中:  
      a^k=ka  
      a^(-k)=-ka  
    * G:group  
      (i) 若ab=c ⇒ (a^-1)ab=(a^-1)c ⇒ eb=(a^-1)c ⇒ b=(a^-1)c  
      (ii)若ab=c ⇒ a=c(b^-1)  
      (iii)若ab=ac ⇒ b=(a^-1)ac=ec=c
 * (S,\*):AS
    * **左消去性** : 若∀a,b,c∈S,a\*b=a\*c ⇒ b=c , 稱(S,\*)具左消去性 
    * **右消去性** : 若∀a,b,c∈S,b\*a=c\*a ⇒ b=c , 稱(S,\*)具右消去性
    * **消去性**   : 若(S\*)具左消及右消 , 稱(S,\*)具消去性    
    → `消去性不一定是由inverse造成的` , 但`有inverse ⇒ 一定有消去性`

 


  
