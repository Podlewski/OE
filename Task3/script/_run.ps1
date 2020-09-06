$FilePath = "data.txt"
$break = "#########################################"

"Wplyw funkcji celu" >> $FilePath
.\Task3.exe -g 1000 -p 1000 -f P >> $FilePath
$break >> $FilePath
.\Task3.exe -g 1000 -p 1000 -f P -r >> $FilePath
$break >> $FilePath
.\Task3.exe -g 1000 -p 1000 -f P -t >> $FilePath
$break >> $FilePath
.\Task3.exe -g 1000 -p 1000 -f P -t -r >> $FilePath
$break >> $FilePath
$break >> $FilePath
.\Task3.exe -g 1000 -p 1000 -f S >> $FilePath
$break >> $FilePath
.\Task3.exe -g 1000 -p 1000 -f S -r >> $FilePath
$break >> $FilePath
.\Task3.exe -g 1000 -p 1000 -f S -t >> $FilePath
$break >> $FilePath
.\Task3.exe -g 1000 -p 1000 -f S -t -r >> $FilePath
$break >> $FilePath

"Wplyw liczby generacji" >> $FilePath
$array = @(50, 100, 250, 500, 1000, 2000)
For ($j = 0; $j -lt $array.length; $j++)
{
    .\Task3.exe -g $array[$j] -p 50 -f P >> $FilePath
}
$break >> $FilePath

"Wplyw liczby populacji" >> $FilePath
$array = @(5, 10, 50, 100, 500, 1000)
For ($j = 0; $j -lt $array.length; $j++)
{
    .\Task3.exe -g 1000 -p $array[$j] -f P >> $FilePath
}
$break >> $FilePath

"Wplyw prawdo mutacji" >> $FilePath
$array = @("0,05", "0,1", "0,2", "0,5", "0,8", "0,9")
For ($j = 0; $j -lt $array.length; $j++)
{
    .\Task3.exe -g 1000 -p 1000 -f P -mc $array[$j] >> $FilePath
}
$break >> $FilePath