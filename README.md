# SIR(Script Inter-language) 

一种服务于脚本执行的可二进制化指令语言

## 支持的虚拟机

SEVM：Script Execution Virtual Machine <https://github.com/inmount/SEVM>

## 指令说明

为了最大化的兼容现有从业者的使用习惯，我将使用类似汇编语言的指令方式定义。

同时，为了简化处理，增加了类似属性值的多样化信息表示。

操作符号说明：

* ; 语句结束符，后面的字符不计入代码
* " 字符串定义符号
* \\ 字符串中的转义符，\"为双引号，\r为回车符，\n为换行符
* @ 标签定义
* ， 命令参数分隔符
* \# 寄存器定义
* $ 临时变量定义
* [] 直接访问虚拟内存


### 一、段指令

#### 1.1 数据定义data及end data

用来定义初始内存信息。

支持的类型包括：number, string

例如：

```
data
    number 1; 添加一个数字虚拟内存空间    
    number 2            
    string "a+b的和是"; 添加一个字符串虚拟内存空间 
    string "print"
end data
```

#### 1.2 变量定义define及end define

用来定义变量名称及指针。

例如：

```
define
    a 6; 定义一个变量a,指向第6个虚拟内存空间
    b 7; 定义一个变量b,指向第7个虚拟内存空间
    c 8
    str 9
end define
```

#### 1.3 代码段定义code及end code

用来定义代码区域

例如：

```
code main
    @print
        call 0, [4]; 将临时变量$1中的所有项目进行连接，再将结果赋值给变量str
        ret 0
    mov a, [1]; 将第11个虚拟内存空间中的内容赋值给变量a
    mov b, [2]
    mov #0, a; 将变量a中的内容赋值给0号寄存器
    add #0, b
    mov c, #0
    ptr $1, 10; 申请一个临时变量$1,并指向第10个虚拟内存空间
    list $1; 设置临时变量$1为一个列表
    ptr $1.0, 3; 临时变量$1列表的第1项指向第3个虚拟内存空间
    ptr $2, 11;
    lea #0, $2; 将临时变量$2的指针赋值给0号寄存器
    ptr $1.1, #0; 临时变量$1列表的第2项指向临时变量$1的虚拟内存空间
    mov $2, c
    mov str, $1; 将临时变量$1中的所有项目进行连接，再将结果赋值给变量str
    ptr $3, 12;
    list $3;
    lea #0, str
    ptr $3.0, #0
    lea #0, $3; 将临时变量$3的地址赋值给0号寄存器
    call 0, print
end code
```

### 二、数据指令

#### 2.1 传送指令 mov

用来传值。

例如：

```
mov #0, 0
```

#### 2.2 申请或变更指针指令 ptr

申请一个临时变量，并指定指针地址，或者更改现有变量的指针地址

例如：

```
ptr $1
ptr $1, 1
ptr $1.1, 1
ptr $1.name, 1
```

#### 2.3 获取指针指令 lea

申请一个临时变量，并指定指针地址，或者更改现有变量的指针地址

例如：

```
lea #0, $1
```

### 三、类型操作指令

#### 3.1 列表指令 list

设定目标内存类型为列表。

例如：

```
list $1
```

#### 3.2 列表连接指令 join

连接后值列表中的所有项目，将结果给前值。

例如：

```
join str, $1
```

#### 3.3 列表统计指令 cnt

计算后值列表中的项目数量，将结果给前值。

例如：

```
cnt #0, $1
```

#### 3.4 对象指令 obj

设定目标内存类型为对象。

例如：

```
obj $1
```

#### 3.5 对象指令 keys

获取所有指令

例如：

```
keys $1, $2
```

### 四、运算操作指令

#### 4.1 加法指令 add

执行加法运算。

例如：

```
add $1, 1
```

#### 4.2 减法指令 sub

执行减法运算。

例如：

```
sub $1, 1
```

#### 4.3 减法指令 mul

执行乘法运算。

例如：

```
mul $1, 1
```

#### 4.4 除法指令 div

执行除法运算。

例如：

```
div $1, 1
```

### 五、逻辑操作指令

#### 5.1 非指令 not

执行非运算。

例如：

```
not $1
```

#### 5.2 与指令 and

执行与运算。

例如：

```
and $1, $2
```

#### 5.3 或指令 or

执行或运算。

例如：

```
or $1, $2
```

#### 5.4 异或指令 xor

执行异或运算。

例如：

```
xor $1, $2
```

### 六、比较指令

#### 6.1 相等比较指令 equal

执行比较运算，判断两个值是否相等，相等为1，不相等为0，比较结果存储到#0。

例如：

```
equal $1, $2
```

#### 6.2 大于比较指令 large

执行比较运算，判断前值是否大于后值，大于为1，否则为0，比较结果存储到#0。

例如：

```
large $1, $2
```

#### 6.3 小于比较指令 small

执行比较运算，判断前值是否大于后值，大于为1，否则为0，比较结果存储到#0。

例如：

```
small $1, $2
```

### 七、区域操作指令

#### 7.1 无条件跳转指令 jmp

执行跳转。

例如：

```
jmp L01
```

#### 7.2 带条件跳转指令 jmpf

当#0寄存器大于0，则执行前值跳转，否则执行后值跳转

例如：

```
mov #0, 1
jmpf L01, L02
```

#### 7.3 返回指令 jet

对应Call执行返回。

例如：

```
jet $1
```