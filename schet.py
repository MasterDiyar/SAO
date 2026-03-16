import os

def count_lines_in_file(file_path):
    """Подсчитывает количество строк, игнорируя ошибки кодировки"""
    try:
        # utf-8-sig обрабатывает файлы с BOM (часто встречается в C#)
        with open(file_path, 'r', encoding='utf-8-sig', errors='ignore') as file:
            return sum(1 for _ in file)
    except Exception as e:
        print(f" Не удалось прочитать {file_path}: {e}")
        return 0

def parse_cs_files(directory):
    stats = {}
    # Проверка на существование директории
    if not os.path.exists(directory):
        print(f"Ошибка: Директория {directory} не найдена.")
        return stats

    for root, dirs, files in os.walk(directory):
        # Игнорируем технические папки Godot и VS, чтобы не портить статистику
        if any(ignored in root for ignored in ['.godot', '.vs', 'bin', 'obj']):
            continue

        folder_stats = {'files': 0, 'lines': 0}
        
        for file in files:
            if file.endswith('.cs'):
                file_path = os.path.join(root, file)
                lines = count_lines_in_file(file_path)
                folder_stats['files'] += 1
                folder_stats['lines'] += lines
        
        if folder_stats['files'] > 0:
            relative_path = os.path.relpath(root, directory)
            stats[relative_path] = folder_stats
    
    return stats

def print_stats(stats):
    if not stats:
        print("C# файлы (.cs) не найдены в текущей директории и подпапках.")
        return

    print("\n{:<50} {:<10} {:<10}".format('Папка (относительно скрипта)', 'Файлы', 'Строки'))
    print("-" * 75)
    
    total_files = 0
    total_lines = 0
    
    # Сортировка по количеству строк
    for folder, data in sorted(stats.items(), key=lambda x: x[1]['lines'], reverse=True):
        print("{:<50} {:<10} {:<10}".format(
            folder if folder != '.' else '[Корень]', 
            data['files'], 
            data['lines']
        ))
        total_files += data['files']
        total_lines += data['lines']
    
    print("-" * 75)
    print("{:<50} {:<10} {:<10}".format('ИТОГО:', total_files, total_lines))

if __name__ == "__main__":
    # АВТОМАТИЧЕСКИЙ ПУТЬ: берем папку, где лежит этот скрипт
    current_dir = os.path.dirname(os.path.abspath(__file__))
    
    print(f"Анализ папки: {current_dir}")
    
    stats = parse_cs_files(current_dir)
    print_stats(stats)