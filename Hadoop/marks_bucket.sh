echo "Enter marks (0-100): "
read marks

if ! [[ "$marks" =~ ^[0-9]+$ ]] || [ "$marks" -gt 100 ] || [ "$marks" -lt 0 ]; then
    echo "Error: Please enter a valid number between 0 and 100"
    exit 1
fi

if [ "$marks" -ge 75 ] && [ "$marks" -le 100 ]; then
    echo "Marks: $marks -> Bucket 1 (Excellent)"
elif [ "$marks" -ge 50 ] && [ "$marks" -lt 75 ]; then
    echo "Marks: $marks -> Bucket 2 (Good)"
elif [ "$marks" -ge 25 ] && [ "$marks" -lt 50 ]; then
    echo "Marks: $marks -> Bucket 3 (Average)"
else
    echo "Marks: $marks -> Needs Improvement"
fi
