lenght = int(input("Enter the lenght of rectangle: "))
breadth = int(input("Enter the breadth of rectangle: "))
area = lenght * breadth
print("Area of rectangle is:", area)

student = {
    "name": "John",
    "age": 21,
    "courses": ["Math", "CompSci"]
}
print (student)

marks = 98
if marks >= 90:
    grade = 'A' 
elif marks >= 80:
    grade = 'B'
else:
    grade = 'C'
print("Grade is:", grade)

for i in range(5):
    print(i)