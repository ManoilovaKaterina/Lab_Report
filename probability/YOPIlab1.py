import matplotlib.pyplot as plt
import time
import math

file10 = open('C:/Users/undor/OneDrive/Рабочий стол/task_01_data/input_10.txt', 'r')
file100 = open('C:/Users/undor/OneDrive/Рабочий стол/task_01_data/input_100.txt', 'r')
file1000 = open('C:/Users/undor/OneDrive/Рабочий стол/task_01_data/input_1000.txt', 'r')

def out(arr):
    print("Список елементів: ", end=" ")
    for el in arr:
        print(el, end=" ")

def median(arr):
    if len(arr)%2 == 0:
        med = (arr[int(len(arr)/2)]+arr[int(len(arr)/2)-1])/2
    else:
        med = (arr[math.trunc(len(arr)/2)])
    return med

freqArr = {}

def findMax(arr):
    print("Найбільше переглядів ", max(arr), " у фільма за індексом ", arr.index(max(arr)))

def countFr(arr, freqarr):
    for el in arr:
        if str(el) in freqarr.keys():
            freqarr[str(el)][0] += 1
        else:
            freqarr[str(el)] = [1, 0]

def countCuFr(freqarr):
    i = 0
    fr = 0
    for el in freqarr.keys():
        fr += freqarr[el][0]
        if i==0:
            freqarr[el][1] = freqarr[el][0]
        else:
            freqarr[el][1] = fr
        i += 1

def findMod(freqarr):
    vallist = []
    for el in freqarr.keys():
        vallist.append(freqarr[el][0])
    mv = max(vallist)

    if mv==1:
        print("Немає моди")
    else:
        for el in freqarr.keys():
            if freqarr[el][0]==mv:
                print("Мода: ", el)

def findMid(arr):
    mid = sum(arr)/len(arr)
    return mid

def MAD(arr, freqarr):
    sum1 = 0
    mid = findMid(arr)
    for el in freqarr.keys():
        sum1 += freqarr[el][0]*abs(int(el)-mid)
    mad = sum1/len(arr)
    return mad

def dispersion(arr, freqarr):
    sum1 = 0
    mid = findMid(arr)
    for el in freqarr.keys():
        sum1 += freqarr[el][0]*(int(el)-mid)**2
    disp = sum1/len(arr)
    return disp

def figure():
    fig = plt.figure()
    ax = fig.add_axes([0.1,0.1,0.8,0.8])
    filmcount = []
    freq = []
    for el in freqArr.keys():
        freq.append(freqArr[el][0])
    for el in freqArr.keys():
        filmcount.append(el)
    ax.bar(filmcount, freq)

def scenario(file):
    start_time = time.time()

    LineString = file.read().splitlines()
    Lines = [int(x) for x in LineString]
    Lines.remove(Lines[0])

    out(Lines)
    print("\nКількість елементів: ", len(Lines))
    findMax(Lines)
    print("Медіана: ", median(Lines))
    countFr(Lines, freqArr)
    countCuFr(freqArr)
    print("Таблиця частот: ")
    print("Елемент | Частота | Відносна частота")
    for el in freqArr.keys():
        print(el, "\t| ", freqArr[el][0], "\t  |\t", freqArr[el][1])
    findMod(freqArr)
    print("Середнє абсолютне відхилення = ", MAD(Lines, freqArr))
    print("Дисперсія = ", dispersion(Lines, freqArr))
    
    figure()
    print("--- час виконання: %s секунд ---" % (time.time() - start_time))
    plt.show()

def menu():
    print("Введіть кількість елементів файлу (10, 100, 1000): ", end=" ")
    comm = input()
    if comm == "10":
        scenario(file10)
    elif comm == "100":
        scenario(file100)
    elif comm == "1000":
        scenario(file1000)
    else:
        print("Введеної кількості елементів не існує, спробуйте ще: ")
        menu()

menu()