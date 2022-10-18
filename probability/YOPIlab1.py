import matplotlib.pyplot as plt
import time
import math

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
    fr = 0
    for el in freqarr.keys():
        fr += freqarr[el][0]
        freqarr[el][1] = fr

def findMod(freqarr, fileout):
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
                fileout.write("\nМода: " + str(el))

def findMid(arr):
    mid = sum(arr)/len(arr)
    return mid

def dispersion(arr, freqarr):
    sum1 = 0
    mid = findMid(arr)
    for el in freqarr.keys():
        sum1 += freqarr[el][0]*(int(el)-mid)**2
    disp = sum1/len(arr)
    return disp

def bardiagram():
    fig = plt.figure()
    ax = fig.add_axes([0.1,0.1,0.8,0.8])
    filmcount = []
    freq = []
    for el in freqArr.keys():
        freq.append(freqArr[el][0])
    for el in freqArr.keys():
        filmcount.append(el)
    ax.bar(filmcount, freq)

def Interval(arr):
    intSize = int(round(max(arr)*0.04))
    intarr = []
    tempint = 0
    while tempint <= max(arr):
        intarr.append(tempint)
        tempint += intSize
    return intarr

def histogram(arr):
    interval = Interval(arr)
    print("Інтервали значень: ", interval)
    plt.hist(sorted(arr), interval, facecolor='r', alpha=0.7, edgecolor='k', linewidth=1)
    plt.title("Частота просмотрів фільмів")
    plt.xlabel("К-ть просмотрів фільмів")
    plt.ylabel("Частота")

def writeOutput(Lines, fileout):
    fileout.write("Елемент | Частота | Сукупна частота\n")
    for el in freqArr.keys():
        tempstring = str(el) + "\t| " + str(freqArr[el][0]) + "\t  |\t" + str(freqArr[el][1]) + "\n"
        fileout.write(tempstring)
        fileout.flush()
    fileout.write("\nКількість елементів: " + str(len(Lines)))
    fileout.flush()
    fileout.write("\nМедіана: " + str(median(Lines)))
    fileout.flush()
    fileout.write("\nДисперсія = " + str(dispersion(Lines, freqArr)))
    fileout.flush()
    fileout.write("\nСереднє квадратичне (стандартне) відхилення = " + str(math.sqrt(dispersion(Lines, freqArr))))
    fileout.flush()

def scenario(file, fileout):
    start_time = time.time()

    LineString = file.read().splitlines()
    Lines = [int(x) for x in LineString]
    Lines.remove(Lines[0])
    Lines = sorted(Lines)

    print("\nКількість елементів: ", len(Lines))
    findMax(Lines)
    print("Медіана: ", median(Lines))
    countFr(Lines, freqArr)
    countCuFr(freqArr)
    print("Таблиця частот: ")
    print("Елемент | Частота | Сукупна частота")
    for el in freqArr.keys():
        print(el, "\t| ", freqArr[el][0], "\t  |\t", freqArr[el][1])
    print("Дисперсія = ", dispersion(Lines, freqArr))
    print("Середнє квадратичне (стандартне) відхилення = ", math.sqrt(dispersion(Lines, freqArr)))
    writeOutput(Lines, fileout)
    findMod(freqArr, fileout)
    fileout.flush()

    fileout.close
    histogram(Lines)
    print("--- час виконання: %s секунд ---" % (time.time() - start_time))
    plt.show()

def menu():
    print("Введіть кількість елементів файлу (10, 100, 1000): ", end=" ")
    comm = input()
    if comm == "10" or comm == "100" or comm == "1000":
        filestring = "C:/Users/undor/OneDrive/Рабочий стол/task_01_data/input_" + comm +".txt"
        fileoutstring = "C:/Users/undor/OneDrive/Рабочий стол/task_01_data/output" + comm +".txt"
        file = open(filestring, 'r')
        fileout = open(fileoutstring, 'w')
        scenario(file, fileout)
    else:
        print("Введеної кількості елементів не існує, спробуйте ще: ")
        menu()

menu()
