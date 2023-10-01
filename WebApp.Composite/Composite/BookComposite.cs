﻿
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text;

namespace WebApp.Composite.Composite
{
    //Hem component hem de composite olabilir.
    public class BookComposite : IComponent
    {
        public int Id { get; set; }
        public string Name { get; set; }

        //composite olduğu için list tutmalı
        private List<IComponent> _components;

        //_component'si dış dunyaya açmış olduk. CategoryMenuController'ın index view'ında erişebileceğiz.
        public IReadOnlyCollection<IComponent> Components => _components;
        public BookComposite(int id, string name)
        {
            Id = id;
            Name = name;
            _components = new List<IComponent>();
        }

        public void Add(IComponent component)
        {
            _components.Add(component);
        }
        public void Remove(IComponent component)
        {
            _components.Remove(component);
        }
        public int Count()
        {
            //Composite'in altında birçok kitap olabilir ve bunları topla
            return _components.Sum(x => x.Count());
        }
        public string Display()
        {
            //her bir kategori bir div olsun
            var sb = new StringBuilder();

            sb.Append($"<div class='text-primary my-1'><a href='#' class='menu'> {Name} ({Count()}) </a> </div>");

            //bu categorynin altında alt kategori olup olmadığını count'a bakarak anlayabilirim

            //Bu componentlerden var mı
            if (!_components.Any()) return sb.ToString();

            //var ise
            sb.Append("<ul class='list-group list-group-flush ms-3'>");

            foreach (var item in _components)
            {
                sb.Append(item.Display());
            }
            sb.Append("</ul>");

            return sb.ToString();
        }

        //Dropdownlisteki her bir item'a karşılık gelmektedir.
        public List<SelectListItem> GetSelectListItems(string line)
        {
            var list = new List<SelectListItem> { new($"{line}{Name}", Id.ToString()) };
            //is = x = bookcompoentn => true
            if (_components.Any(x => x is BookComposite))
            {
                line += " - ";
            }
            _components.ForEach(x =>
            {
                if (x is BookComposite bookComposite)
                {
                    list.AddRange(bookComposite.GetSelectListItems(line)); //recursive
                }
            });
            return list;
        }
    }
}
