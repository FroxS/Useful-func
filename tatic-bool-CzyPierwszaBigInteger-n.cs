// Liczba pierwsza
static bool CzyPierwszav1(BigInteger n)
{
    if (n < 2) return false;
    if (n <= 3) return true;
    if (n % 2 == 0 || n % 3 == 0) return false;
    for (ulong i = 5, j = 7; i * i <= n; i += 6, j += 6)
    {
        if (n % i == 0 || n % j == 0) return false;
    }
    return true;
}

static bool CzyPierwsza(BigInteger n)
{
    if (n < 2) return false;
    else if (n < 4) return true;
    else if (n % 2 == 0) return false;
    else for (BigInteger u = 3; u < n / 2; u += 2)
    {
        if (n % u == 0) return false;
    }
    return true;
}

//SORTOWANIA

//Sortowanie przez wstawianie

 static void InsertionSort(int[] t) 
{
    for (uint i = 1; i < t.Length; i++)
    {
        uint j = i; // elementy 0 .. i-1 są już posortowane
        int Buf = t[j]; // bierzemy i-ty (j-ty) element
        while ((j > 0) && (t[j - 1] > Buf))
        { // przesuwamy elementy
            t[j] = t[j - 1];
            j--;
        }
        t[j] = Buf; // i wpisujemy na docelowe miejsce
    }
} 

//Sortowanie przez wybieranie

static void SelectionSort(int[] t)
{
    uint k;
    for (uint i = 0; i < (t.Length - 1); i++)
    {
        int Buf = t[i]; // bierzemy i-ty element
        k = i; // i jego indeks
        for (uint j = i + 1; j < t.Length; j++)
            if (t[j] < Buf) // szukamy najmniejszego z prawej
            {
                k = j;
                Buf = t[j];
            }
        t[k] = t[i]; // zamieniamy i-ty z k-tym
        t[i] = Buf;
    }
}

//Sortowanie koktajlowe

static void CocktailSort(int[] t)
{
    int Left = 1, Right = t.Length - 1, k = t.Length - 1;
    do
    {
        for (int j = Right; j >= Left; j--) // przesiewanie od dołu
            if (t[j - 1] > t[j])
            {
                int Buf = t[j - 1]; t[j - 1] = t[j]; t[j] = Buf;
                k = j; // zamiana elementów i zapamiętanie indeksu
            }
        Left = k + 1; // zacieśnienie lewej granicy
        for (int j = Left; j <= Right; j++) // przesiewanie od góry
            if (t[j - 1] > t[j])
            {
                int Buf = t[j - 1]; t[j - 1] = t[j]; t[j] = Buf;
                k = j; // zamiana elementów i zapamiętanie indeksu
            }
        Right = k - 1; // zacieśnienie prawej granicy
    }
    while (Left <= Right);
}

//Sortowanie przez kopcowanie

static void HeapSort(int[] t)
{
    uint left = ((uint)t.Length / 2),
    right = (uint)t.Length - 1;
    while (left > 0) // budujemy kopiec idąc od połowy tablicy
    {
        left--;
        Heapify(t, left, right);
    }
    while (right > 0) // rozbieramy kopiec
    {
        int buf = t[left];
        t[left] = t[right];
        t[right] = buf; // największy element
        right--; // kopiec jest mniejszy
        Heapify(t, left, right); // ale trzeba go naprawić
    }
} 
// Budowanie kopca
static void Heapify(int[] t, uint left, uint right)
{ // procedura budowania/naprawiania kopca
    
    uint i = left,
    j = 2 * i + 1;
    int buf = t[i]; // ojciec
    while (j <= right) // przesiewamy do dna stogu
    {
        if (j < right) // wybieramy większego syna
            if (t[j] < t[j + 1]) j++;
        if (buf >= t[j]) break;
        t[i] = t[j];
        i = j;
        j = 2 * i + 1; // przechodzimy do dzieci syna
    }
    t[i] = buf;
} 

// Quick sort rekurencyjnie piwot srodkowy

static void qsortPS(int[] tab, int lewy, int prawy)
{
    int i, j, pivot;
    i = (lewy + prawy) / 2;
    pivot = tab[i];
    tab[i] = tab[prawy];
    j = 0;
    for (j = i = lewy; i < prawy; i++)
    {
        if (tab[i] < pivot)
        {
            int tmp = tab[j];
            tab[j] = tab[i];
            tab[i] = tmp;
            j++;
        }
    }
    tab[prawy] = tab[j];
    tab[j] = pivot;
    if (lewy < j - 1) qsortPS(tab, lewy, j - 1);
    if (j + 1 < prawy) qsortPS(tab, j + 1, prawy);
}

// Quick sort rekurencyjnie piwot losowy

static void qsortPL(int[] tab, int lewy, int prawy)
{
    int i, j, pivot;
    Random rand = new Random();
    i = rand.Next(lewy, prawy + 1);
    pivot = tab[i];
    tab[i] = tab[prawy];
    j = 0;
    for (j = i = lewy; i < prawy; i++)
    {
        if (tab[i] < pivot)
        {
            int tmp = tab[j];
            tab[j] = tab[i];
            tab[i] = tmp;
            j++;
        }
    }
    tab[prawy] = tab[j];
    tab[j] = pivot;
    if (lewy < j - 1) qsortPL(tab, lewy, j - 1);
    if (j + 1 < prawy) qsortPL(tab, j + 1, prawy);
}

// Quick sort iteracyjnie piwot srodkowy

static void qsortIterPS(int[] t)
{
    int i, j, l, p, sp;
    int[] stos_l = new int[t.Length],
    stos_p = new int[t.Length]; // przechowywanie żądań podziału
    sp = 0; stos_l[sp] = 0; stos_p[sp] = t.Length - 1; // rozpoczynamy od całej tablicy
    do
    {
        l = stos_l[sp]; p = stos_p[sp]; sp--; // pobieramy żądanie podziału
        do
        {
            int x;
            i = l;
            j = p;
            x = t[(l + p) / 2]; // piwot srodkowy
            do
            {
                while (t[i] < x) i++;
                while (x < t[j]) j--;
                if (i <= j)
                {
                    //instru++;
                    int buf = t[i];
                    t[i] = t[j];
                    t[j] = buf;
                    i++;
                    j--;
                }
            } while (i <= j);
            if (i < p)
            {
                sp++;
                stos_l[sp] = i;
                stos_p[sp] = p;
            } // ewentualnie dodajemy żądanie podziału
            p = j;
        } while (l < p);
    } while (sp >= 0); // dopóki stos żądań nie będzie pusty
} 

//Wyszukiwania

// Wyszukiwanie liniowe
static bool IsPresent_Linear(int[] Vector, int Number)
{
    for (int i = 0; i < Vector.Length; i++)
        if (Vector[i] == Number)
            return true;
    return false;
}


// Wyszukiwanie binarne
static bool IsPresent_Binary(int[] Vector, int Number)
{
    int Left = 0, Right = Vector.Length - 1, Middle;
    while (Left <= Right)
    {
        Middle = (Left + Right) / 2;
        if (Vector[Middle] == Number) return true;
        else if (Vector[Middle] > Number) Right = Middle - 1;
        else Left = Middle + 1;
    }
    return false;
}
