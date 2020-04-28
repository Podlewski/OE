$path = "result.txt"

$algorithms = 3
$test_problems = 3

'Alg.      Test P.   Gen.      Pop.      Prop.     GD        HV' >> $path

for ($a = 1; $a -le $algorithms; $a++)
{
    for ($t = 1; $t -le $test_problems; $t++)
    {
        foreach($g in @(1000, 10000, 100000))
        {
            foreach($p in @(50, 100, 200))
            {
                foreach($r in @(0.25, 0.5, 0.75))
                {
                    if (($a -ne 2) -or ($g -ne 100000) -or ($p -ne 200))
                    {
                        python ../main.py $a $t -g $g -p $p -r $r --screen >> $path
                    }
                }
            }
        }
    }
}

'Alg.      Test P.   Gen.      Pop.      Prop.     E         GD        HV' >> $path

for ($t = 1; $t -le $test_problems; $t++)
{
    for ($e = 1; $e -le 3; $e++)
    {
        python ../main.py 2 $t -g 10000 -p 50 -r 0.25 -e $e --screen >> $path
    }
}

for ($t = 1; $t -le $test_problems; $t++)
{
    foreach($e in @(4, 8, 12))
    {
        python ../main.py 3 $t -g 10000 -p 50 -r 0.25 -e $e  --screen >> $path
    }
}
