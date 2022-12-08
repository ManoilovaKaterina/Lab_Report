import matplotlib.pyplot as plt
import time
import math 

def out(arr):
    print("Оцінки: ", end=" ")
    for el in arr:
        print(el, end=" ")

def median(arr):
    if len(arr)%2 == 0:
        med = (arr[int(len(arr)/2)]+arr[int(len(arr)/2)-1])/2
    else:
        med = (arr[math.trunc(len(arr)/2)])
    return med

def findMean(arr):
    mid = sum(arr)/len(arr)
    return mid

def Percentile(arr, k): 
    perIn = (k/100)*(len(arr)+1) - 1
    per = arr[math.trunc(perIn)]+(perIn%1)*(arr[math.trunc(perIn)+1]-arr[math.trunc(perIn)])
    return per

def countZScore(arr):
    zscore = []
    mid = findMean(arr)
    for el in arr:
        zscore.append(round((el-mid)/stanDev(arr), 3))
    return zscore

def stanDev(arr): 
    sum1 = 0
    mid = findMean(arr)
    for el in arr:
        sum1 += (int(el)-mid)**2
    var = sum1/len(arr)
    return math.sqrt(var)

def correctGrades(arr, wMax, wMean):
    newGrades = []
    a = (wMax - wMean)/(wMax - findMean(arr))
    b = wMax - wMax*a
    for el in arr:
        newGrades.append(round(el*a+b, 3))
    return newGrades

def StemAndLeaf(arr):
    stemarr = {}
    temp = range(math.trunc(min(arr)/10), math.trunc(max(arr)/10)+1)
    for el in temp:
        stemarr[str(el)] = []
    for el in arr:
        stemarr[str(math.trunc(el/10))].append((el%10))
    return stemarr

def boxplot(arr):
    item = {}

    item["med"] = median(arr)
    item["q1"] = Percentile(arr, 25)
    item["q3"] = Percentile(arr, 75)
    item["whislo"] = min(arr)
    item["whishi"] = max(arr)
    item["fliers"] = []

    stats = [item]

    fig, axes = plt.subplots(1, 1)
    axes.bxp(stats)
    axes.set_title('Коробковий графік')
    axes.grid(axis = 'y')
    y_axis = range(min(arr), max(arr)+1, 5)
    y_values = []
    for el in y_axis:
        y_values.append(str(el))
    plt.yticks(y_axis, y_values)
    plt.savefig('C:/Users/undor/OneDrive/Рабочий стол/task_02_data/boxplot.png')

def writeOutput(Grades, z, cor, stem, fileout):
    fileout.write("Q1= " + str(Percentile(Grades, 25)))
    fileout.flush()
    fileout.write("\nQ3 =  " + str(Percentile(Grades, 75)))
    fileout.flush()
    fileout.write("\nP90 = " + str(Percentile(Grades, 90)))
    fileout.flush()
    fileout.write("\nСтандартне відхилення = " + str(stanDev(Grades)))
    fileout.flush()

    fileout.write("\nZ-оцінки: ")
    for el in z:
        fileout.write(str(el) + " ")
        fileout.flush()

    fileout.write("\nВиправлені оцінки, при умові, що: 100 = 100, середня оцінка = 95: ")
    for el in cor:
        fileout.write(str(el) + " ")
        fileout.flush()
    
    fileout.write("\nСтовбур\t| Листя\n")
    for el in stem.keys():
        tempstring = str(el) + "\t| "
        for i in stem[el]:
            tempstring += str(i) + " "
        tempstring += "\n"
        fileout.write(tempstring)
        fileout.flush()

def scenario(file, fileout):
    start_time = time.time()

    GradeString = file.read().splitlines()
    Grades = [int(x) for x in GradeString]
    Grades.remove(Grades[0])
    Grades = sorted(Grades)

    print("\nОцінки: ")
    print(Grades)

    print("Q1 = ", Percentile(Grades, 25))
    print("Q3 = ", Percentile(Grades, 75))
    print("P90 = ", Percentile(Grades, 90))
    print("Стандартне відхилення = ", stanDev(Grades))

    print("Z-оцінки: ")
    zscore = countZScore(Grades)
    print(zscore)

    print("Виправлені оцінки, при умові, що: 100 = 100, середня оцінка = 95: ")
    corGr = correctGrades(Grades, 100, 95)
    print(corGr)

    stemarr = StemAndLeaf(Grades)
    print("Стовбур\t| Листя")
    for el in stemarr.keys():
        print(el, "\t| ", *stemarr[el])

    writeOutput(Grades, zscore, corGr, stemarr, fileout)
    fileout.flush()
    fileout.close

    boxplot(Grades)
    print("--- час виконання: %s секунд ---" % (time.time() - start_time))
    plt.show()

def menu():
    print("Введіть кількість елементів файлу (10, 100): ", end=" ")
    comm = input()
    if comm == "10" or comm == "100":
        filestring = "C:/Users/undor/OneDrive/Рабочий стол/task_02_data/input_" + comm +".txt"
        fileoutstring = "C:/Users/undor/OneDrive/Рабочий стол/task_02_data/output" + comm +".txt"
        file = open(filestring, 'r')
        fileout = open(fileoutstring, 'w')
        scenario(file, fileout)
    else:
        print("Введеної кількості елементів не існує, спробуйте ще: ")
        menu()

menu()