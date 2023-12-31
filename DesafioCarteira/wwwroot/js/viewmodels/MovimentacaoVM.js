﻿function MovimentacaoVM(id, pessoa, data, desc, valor) {
    var self = this;
    self.Id = ko.observable(id || "");
    self.Pessoa = ko.observable(pessoa || "");
    self.Data = ko.observable(data || getNewDate(new Date()));
    self.Descricao = ko.observable(desc || "");
    self.Valor = ko.observable(valor || "");

    self.resetFields = () => {
        self.Id("");
        self.Pessoa("");
        self.Data(getNewDate(new Date()));
        self.Descricao("");
        self.Valor("");
    }
}