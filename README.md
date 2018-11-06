# APKINFO
APKINFO是一款APK信息查看工具，除了查看APK包名、应用名、各版本号、权限、assets下配置文件参数之外，还有查看APK签名信息、查看源码（集成第三方jadx代码查看器）、查看签名库文件、
反编译、重签名、打入Jar包文件到APK功能，方便用户快速排查问题，尤其是从事游戏SDK接入或打包的童鞋，根据工作需求来制作一款工具能大大提升工作效率。

![APKINFO图片](https://img-blog.csdnimg.cn/20181101193734294.png "APKINFO图片") 
![打入Jar文件图片](https://img-blog.csdnimg.cn/2018110517214644.png "打入Jar文件图片")


# 项目依赖工具
项目中引入了JDK和Android ADT部分工具
aapt.exe、jadx-0.7.1、keytool.exe、java.exe、apktool.jar、apktool.bat、jarsigner.exe、zipalign.exe、dx.jar、baksmali.jar、android.jar、jarsigner.exe、zipalign.exe
<br><br>
除了jadx-0.7.1放在项目根目录外，其他工具放在项目根目录/win下

# APKINFO各功能命令行

  * 查看APK信息
    * aapt dump badging apkFilePath

  * 查看源代码
    * jadx-gui.bat  apkFilePath 或jarFilePath 或dexFilePath

  * 查看签名信息
    * keytool  -printcert  -file  apk中META-INF目录下的.RSA签名文件路径  // 查看APK签名信息
    * keytool  -list -v -keystore certFilePath  -storepass  password  // 查看.jks或.keystore签名库信息

  * 反编译
    * apktool.bat  d  apkFilePath  -o  输出目录

  * 重签名
    * 1.jarsigner  -digestalg SHA1 -sigalg SHA1withRSA  -keystore  keystoreFilePath   -storepass  password   -keypass aliasPwd   apkFilePath  aliasKey
    * 2.zipalign  -f 4  apkFilePath   newApkFilePath

  * 打入Jar包
    * 1.apktool.bat  d  apkFilePath  -o  输出目录decompileDir  //反编译
    * 2.java  -jar -Xms1024m -Xmx1024m  dxJarFilePath  --dex --output=dexFilePath  jarFilePath  //将jar包转换成dex文件
    * 3.java  -jar baksmaliJarFilePath  -o  targetDir dexFilePath  //将dex文件转换成smali文件
    * 4.apktool.bat  b  decompileDir  -o  apkFilePath  //回编译生成apk
    * 5.jarsigner  -digestalg SHA1 -sigalg SHA1withRSA  -keystore  keystoreFilePath   -storepass  password   -keypass aliasPwd   apkFilePath  aliasKey  //签名
    * 6.zipalign  -f 4  apkFilePath   newApkFilePath  //对齐优化

# 编程思路
编程思路主要是：C#程序调用CMD执行命令行，通过重定向输出获得命令行执行后的数据，并处理该数据。
<br><br>
APKINFO各个功能点都是依靠执行项目中引入的这些工具来实现的。
<br><br>
业务逻辑：当用户拖放文件到APKINFO.exe，或用户双击APKINFO.exe弹出文件选择框选择文件后，程序获得文件路径，并判断该文件的扩展名，如果是.jar或.dex文件，则调用jadx工具打开查看源码功能；如果是.jks或.keystorey
文件，则打开查看签名库文件信息界面；如果是.apk文件，则打开查看APK信息界面，该界面底部提供对APK相关操作的功能按钮，包括查看APK源码、查看APK签名信息、
APK反编译、APK重签名、打入Jar包文件到APK功能，点击其中一个功能按钮，程序会传递APK路径进入该功能操作。
<br><br>
项目结构：
<br>
![项目结构图片](https://img-blog.csdnimg.cn/20181105165210420.png "项目结构图片")

# License
```
Copyright (C) 2018 ZhongGuanYao <598115778@qq.com>

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

     http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.
```
