namespace App.Lab4;

public class Triangle : IGeometry
{
    private readonly I2DVertex[] vertices;

    public Triangle(I2DVertex a, I2DVertex b, I2DVertex c)
    {
        vertices = [a, b, c];
    }

    public int VertexCount => vertices.Count();

    public double CalculateArea()
    {
        // Площадь вычисляется через векторное произведение
        // Подробнее см. https://ru.wikipedia.org/wiki/Треугольник#Площадь_треугольника формула 13
        var x1 = vertices[1].X - vertices[0].X;
        var y1 = vertices[1].Y - vertices[0].Y;

        var x2 = vertices[2].X - vertices[0].X;
        var y2 = vertices[2].Y - vertices[0].Y;

        return Math.Abs(x1 * y2 - x2 * y1) / 2;
    }
}
