import csv

# Specify the input and output file paths
input_csv_path = 'C:/Users\Zeeshan Khalid\Desktop\omicron-unity\Assets/athyg_cleaned.csv'
output_csv_path = 'C:/Users\Zeeshan Khalid\Desktop\omicron-unity\Assets/athyg_cleaned1.csv'

# Open input CSV for reading
with open(input_csv_path, 'r') as input_csv:
    # Open output CSV for writing
    with open(output_csv_path, 'w', newline='') as output_csv:
        reader = csv.reader(input_csv)
        writer = csv.writer(output_csv)

        # Write pre-defined field names (if needed)
        writer.writerow(['FIELD1_', 'FIELD2_', 'FIELD3_', 'FIELD4_'])

        # Loop through input CSV rows
        for row in reader:
            # Check if the row is not entirely blank
            if row or any(row) or any(field.strip() for field in row):
                writer.writerow(row)

print(f"Blank rows removed. Output saved to {output_csv_path}")
