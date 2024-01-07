
TCP veya UDP haberleşme protokolleri ile haberleşebilen consol uygulmalarıdır. 1 Client 1 Udp ve 1 TCP Server projesi vardır.

Mesajlar Sezar Şifrelemesi yapılarak gönderilir ve deşifre edilir. Kaydırma işlemi ASCII tablosuna göre yapılmıştır. 

Mesajlar taşınırken [Data Uzunluğu (2byte)] [Mesaj] [Türkçe karakter destek bilgisi(1 bit en yüksek değerli bit)+ShiftKey(7 bit)] şeklinde taşınır.

Mesaj datasında Türkçe karakter destekli mesajlar taşınırken karakterler 2 byte olarak yer kaplar tersi durumda 1 bytelık yer kaplarlar.

Türkçe Karakter Destek bilgisi + Shift Key datası örneğin [0b1000 001] shift key değeri 1 olanve türkçe karakter desteklediğini beliritir.

