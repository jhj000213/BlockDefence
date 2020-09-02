using System;

/// <summary>
/// 랜덤한 인덱스에 값을 저장해주는 배열
/// </summary>
/// <typeparam name="T">배열을 제작할 임의의 자료형</typeparam>
public class RandomLinearArray<T>
{
    T[] array = null;
    bool[] checker;
    int nowCapacity;
    Random rand;

    /// <summary>
    /// 배열 생성(각 배열의 값은 default 값이 할당됨)
    /// </summary>
    /// <param name="length">생성할 배열의 길이</param>
    public RandomLinearArray(int length)
    {
        array = new T[length];
        checker = new bool[length];
        nowCapacity = length;
        rand = new Random();
    }
    /// <summary>
    /// 배열 생성(각 배열의 값은 defaultValue 값이 할당됨)
    /// </summary>
    /// <param name="length">생성할 배열의 길이</param>
    /// <param name="defalutValue">생성할 배열에 넣을 초기값</param>
    public RandomLinearArray(int length,T defalutValue) : this(length)
    {
        for (int i = 0; i < length; i++)
            array[i] = defalutValue;
    }

    /// <summary>
    /// 배열의 랜덤한 인덱스에 값 추가
    /// </summary>
    /// <param name="value">추가될 값</param>
    public void Insert(T value)
    {
        if (nowCapacity < 1)
            return;

        int index = rand.Next(0, nowCapacity);

        for(int i=0;i<=index;i++)
            if(checker[i])
                index++;

        array[index] = value;
        checker[index] = true;
        nowCapacity--;
    }

    /// <summary>
    /// 배열의 특정 위치의 값을 삭제함. (default값으로 바뀜)
    /// </summary>
    /// <param name="index">삭제할 배열의 특정 위치</param>
    public void RemoveAt(int index)
    {
        array[index] = default;
        checker[index] = false;
        nowCapacity++;
    }

    /// <summary>
    /// 저장되어있는 배열을 가져옴.
    /// </summary>
    /// <returns></returns>
    public T[] GetArray()
    {
        if (array != null)
            return array;
        else
            return null;
    }

    /// <summary>
    /// 배열의 사용가능한 빈 공간의 칸 수를 반환함
    /// </summary>
    /// <returns>배열의 사용가능한 빈 공간의 칸 수</returns>
    public int GetCapacity() { return nowCapacity; }

    //사용하기 편하도록 이름만 다른 함수를 만듬
    /// <summary>
    /// 배열의 사용가능한 빈 공간의 칸 수를 반환함
    /// </summary>
    /// <returns>배열의 사용가능한 빈 공간의 칸 수</returns>
    public int GetEmptyCount() { return nowCapacity; }
}
