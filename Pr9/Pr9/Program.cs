using System;
using System.Collections;
using System.Collections.Generic;

namespace Pr9
{
    public class LinkedList : IEnumerable<int>
    {
        /// <summary>
        /// Первый элемент списка
        /// </summary>
        public Item Head { get; private set; }

        /// <summary>
        /// Последний элемент списка
        /// </summary>
        public Item Tail { get; private set; }

        /// <summary>
        /// Количество элементов в списке
        /// </summary>
        public int Count
        {
            get
            {
                if (Head == null)
                    return 0;

                int count = 0;
                Item tmp = Head;
                while (tmp != null)
                {
                    count++;
                    tmp = tmp.Next;
                }
                return count;
            }
        }

        /// <summary>
        /// Создание пустого списка
        /// </summary>
        public LinkedList()
        {
            Initialize(null);
        }

        /// <summary>
        /// Создание списка с заданным числом элементов (данные элементов заполняются по умолчанию)
        /// </summary>
        /// <param name="capacity">Размер списка</param>
        public LinkedList(int capacity)
        {
            if (capacity < 0)
            {
                throw new IndexOutOfRangeException(nameof(capacity));
            }
            if (capacity == 0)
            {
                Initialize(null);
                return;
            }

            Item newItem = new Item();
            Initialize(newItem);

            for (int i = 1; i < capacity; i++)
            {
                Item tmp = new Item();
                Tail.Next = tmp;
                Tail = tmp;
            }
        }

        /// <summary>
        /// Создание списка с заданным числом элементов и заданным значением элементов
        /// </summary>
        /// <param name="capacity">Размер списка</param>
        /// <param name="data">Данные в каждой ячейке</param>
        public LinkedList(int capacity, int data)
        {
            if (capacity < 0)
            {
                throw new IndexOutOfRangeException(nameof(capacity));
            }
            if (capacity == 0)
            {
                Initialize(null);
                return;
            }

            Item item = new Item(data);
            Initialize(item);

            for (int i = 1; i < capacity; i++)
            {
                Item tmp = new Item(data);
                Tail.Next = tmp;
                Tail = tmp;
            }
        }

        /// <summary>
        /// Создание списка на основе другого списка такого же типа
        /// </summary>
        /// <param name="sample">Список, взятый за основу</param>
        public LinkedList(IEnumerable<int> sample)
        {
            bool isFirstIteration = true;

            foreach (int data in sample)
            {
                Item newItem = new Item(data);

                if (isFirstIteration)
                {
                    Initialize(newItem);
                    isFirstIteration = false;
                    continue;
                }

                Tail.Next = newItem;
                Tail = newItem;
            }
        }

        public int this[int index]
        {
            get
            {
                if (Head == null)
                {
                    throw new NullReferenceException();
                }

                if (index < 0 || index >= Count)
                {
                    throw new IndexOutOfRangeException(nameof(index));
                }

                Item tmp = Head;

                for (int i = 0; i < Count; i++)
                {
                    if (i == index)
                        return tmp.Data;
                    tmp = tmp.Next;
                }

                return default;
            }

            set
            {
                if (Head == null)
                {
                    throw new NullReferenceException();
                }

                if (index < 0 || index >= Count)
                {
                    throw new IndexOutOfRangeException(nameof(index));
                }

                Item newItem = new Item(value);

                if (index == 0)
                {
                    Item tmp = Head.Next;
                    Head = newItem;
                    Head.Next = tmp;
                    return;
                }

                Item current = Head.Next;
                Item previous = Head;

                for (int i = 0; i < Count - 1; i++)
                {
                    if (i == index - 1)
                    {
                        Item tmp = current.Next;
                        previous.Next = newItem;
                        newItem.Next = tmp;
                        current = newItem;
                    }
                    current = current.Next;
                    previous = previous.Next;
                }
            }
        }

        /// <summary>
        /// Вывод элементов списка в консоль
        /// </summary>
        public void Print()
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write(" >> ");

            if (Head == null)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("LIST IS EMPTY ");
                Console.ResetColor();
                return;
            }

            Item tmp = Head;

            while (tmp != null)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(tmp + " ");
                tmp = tmp.Next;
            }
            Console.WriteLine();
            Console.ResetColor();
        }

        /// <summary>
        /// Добавление элемента в конец списка
        /// </summary>
        /// <param name="data">Добавляемый элемент</param>
        public void AddRight(int data)
        {
            Item newItem = new Item(data);

            if (Head != null)
            {
                Tail.Next = newItem;
                Tail = newItem;
            }
            else
            {
                Initialize(newItem);
            }
        }

        /// <summary>
        /// Добавление элемента в начало списка
        /// </summary>
        /// <param name="data">Добавляемый элемент</param>
        public void AddLeft(int data)
        {
            Item newItem = new Item(data);

            if (Head != null)
            {
                newItem.Next = Head;
                Head = newItem;
            }
            else
            {
                Initialize(newItem);
            }
        }

        /// <summary>
        /// Добавление элемента на заданную позицию (элемент, стоявший на заданной позиции будет сдвинут)
        /// </summary>
        /// <param name="index">Позиция элемента</param>
        /// <param name="data">Добавляемый элемент</param>
        public void AddAt(int index, int data)
        {
            Item newItem = new Item(data);

            if (Head == null && index == 0)
            {
                Initialize(newItem);
                return;
            }

            if (index == 0)
            {
                newItem.Next = Head;
                Head = newItem;
                return;
            }

            if (index < 0 || index >= Count)
            {
                throw new IndexOutOfRangeException(nameof(index));
            }

            Item tmp = Head;

            for (int i = 0; i < Count; i++)
            {
                if (i == index - 1)
                {
                    newItem.Next = tmp.Next;
                    tmp.Next = newItem;
                    return;
                }
                tmp = tmp.Next;
            }
        }

        /// <summary>
        /// Возвращает ссылку на элемент из списка
        /// </summary>
        /// <param name="data">Значение элемента</param>
        /// <returns>Ссылка на элемент с заданным значением</returns>
        public int Find(int data)
        {
            Item tmp = Head;

            while (tmp != null)
            {
                if (tmp.Data == data)
                    return tmp.Data;
                tmp = tmp.Next;
            }
            return default;
        }

        /// <summary>
        /// Удаление из списка первого вхождения элемента с заданным значением
        /// </summary>
        /// <param name="data">Значение элемента</param>
        /// <returns>Был ли удален элемент</returns>
        public bool Remove(int data)
        {
            if (Head == null)                   //если головной элемент пуст, то весь список также пуст
            {
                return false;
            }

            if (Head.Data == data)              //если первый объект равен заданному значению, то "удаляем" его и завершаем функцию
            {
                Head = Head.Next;
                return true;
            }

            Item previous = Head;
            Item current = Head.Next;

            while (current != null)
            {
                if (current.Data == data)
                {
                    previous.Next = current.Next;
                    return true;
                }
                previous = previous.Next;
                current = current.Next;
            }

            return false;
        }

        /// <summary>
        /// Удаление из списка всех вхождений элементов с заданным значением
        /// </summary>
        /// <param name="data">Значение элементов</param>
        /// <returns>Был ли удален хотя бы один элемент</returns>
        public bool RemoveAll(int data)
        {
            if (Head == null)                        //если пуст головной элемент, то пуст и весь список
                return false;

            LinkedList newList = new LinkedList();   //новый список
            Item tmp = Head;                         //текущий элемент
            bool isRemoved = false;

            while (tmp != null)                      //пока не конец списка
            {
                if (tmp.Data != data)                //если текущий объект не хранит заданное значение
                    newList.AddRight(tmp.Data);      //добавляем его в новый список
                else
                    isRemoved = true;
                tmp = tmp.Next;                      //переходим на следующий объект текущего списка
            }

            Head = newList.Head;                     //
            Tail = newList.Tail;                     //присвоение нового списка текущему списку

            return isRemoved;
        }

        /// <summary>
        /// Удаление из списка элемента с заданным индексом
        /// </summary>
        public void RemoveAt(int index)
        {
            if (index < 0 || index >= Count)
            {
                throw new IndexOutOfRangeException(nameof(index));
            }
            if (index == 0)
            {
                Head = Head.Next;
                return;
            }

            Item previous = Head;
            Item current = Head.Next;

            for (int i = 0; i < Count; i++)
            {
                if (i == index - 1)
                {
                    previous.Next = current.Next;
                    return;
                }
                previous = previous.Next;
                current = current.Next;
            }
        }

        /// <summary>
        /// Удаление из списка множества элементов
        /// </summary>
        /// <param name="startIndex">Номер с которого начинается удаление</param>
        /// <param name="count">Количество удаляемых элементов</param>
        public void RemoveRange(int startIndex, int count)
        {
            if (startIndex < 0 || startIndex >= Count || (startIndex + count) > Count)
            {
                throw new IndexOutOfRangeException(nameof(startIndex));
            }
            if (count == 0)
            {
                return;
            }
            if (count < 0)
            {
                throw new NegativeCountException(nameof(count));
            }

            Item tmp = Head;

            for (int i = 0; i < Count; i++)
            {
                if (i == startIndex)
                {
                    for (int j = 0; j < count; j++)
                        this.RemoveAt(i);
                }
                tmp = tmp.Next;
            }
        }

        /// <summary>
        /// Удаление всех объектов из списка
        /// </summary>
        public void Clear()
        {
            Initialize(null);
        }

        /// <summary>
        /// Инициализация элементов Head и Tail заданным объектом
        /// </summary>
        /// <param name="item"></param>
        private void Initialize(Item item)
        {
            Head = item;
            Tail = item;
        }

        //Обобщенный нумератор
        public IEnumerator<int> GetEnumerator()
        {
            Item tmp = Head;

            while (tmp != null)
            {
                yield return tmp.Data;
                tmp = tmp.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
