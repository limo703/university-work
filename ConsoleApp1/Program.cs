int[] a = new int[8] {1,2,3,4,5,6,7,8};
int[] b = new int[8] { 9, 2, 3, 4, 4, 6, 7, 8 };
int[] c = new int[16];
for(int i = 0; i < 16; i+=2)
{
    c[i] = a[i / 2];
    c[i + 1] = b[i / 2];
}
foreach (int x in c) Console.Write(x);