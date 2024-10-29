namespace crud.produto.Models
{
    public class Item
    {
        public int? Id { get; set; }
        public required string Nome { get; set; }
        public required decimal Preco { get; set; }
    }
}
