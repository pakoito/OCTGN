﻿using Octgn.ProxyGenerator.Definitions;
using Octgn.ProxyGenerator.Mappings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Octgn.ProxyGenerator
{
    public class TemplateSelector
    {
        private List<CardDefinition> templates = null;

        public string DefaultID { get; set; }
        public bool UseMultiFieldMatching { get; set; }

        private List<TemplateMapping> templateMappings;

        public TemplateSelector()
        {
            templates = new List<CardDefinition>();
            templateMappings = new List<TemplateMapping>();
        }

        public void AddTemplate(CardDefinition cardDef)
        {
            if (templates.Contains(cardDef))
            {
                templates.Remove(cardDef);
            }
            templates.Add(cardDef);
        }

        public CardDefinition GetTemplate(Dictionary<string,string> values)
        {
            string field = templateMappings[0].Field;
            if (field != null && values.ContainsKey(field))
            {
                return GetTemplate(values[field]);
            }
            return GetDefaultTemplate();
        }

        public CardDefinition GetTemplate(string ID)
        {
            CardDefinition ret = GetDefaultTemplate();
            foreach (CardDefinition cardDef in templates)
            {
                if (cardDef.id == ID)
                {
                    ret = cardDef;
                    break;
                }
            }
            return (ret);
        }

        public CardDefinition GetDefaultTemplate()
        {
            foreach (CardDefinition cardDef in templates)
            {
                if (cardDef.id == DefaultID)
                {
                    return cardDef;
                }
            }
            return null;
        }

        public void ClearTemplates()
        {
            templates.Clear();
        }

        public void AddMapping(string field)
        {
            TemplateMapping mapping = new TemplateMapping()
            {
                Field = field
            };
            if (ContainsMapping(mapping))
            {
                templateMappings.Remove(mapping);
            }
            templateMappings.Add(mapping);
        }

        public bool ContainsMapping(string field)
        {
            TemplateMapping mapping = new TemplateMapping()
            {
                Field = field
            };
            return (ContainsMapping(mapping));
        }

        public bool ContainsMapping(TemplateMapping mapping)
        {
            return (templateMappings.Contains(mapping));
        }
    }
}
