angular.module('app', ["ngMaterial"]);

angular.module('app').controller('AppController', function ($scope, $http) {
    $scope.alimentos = [];

    popularLista();

    function popularLista() {
        $scope.carregando = true;
        $http.get('http://localhost:59266/listacompra/BuscarListaCompra')
            .then(function (response) {
                $scope.carregando = false;
                $scope.alimentos = response.data;

            })
            .catch(function (response) {
                console.log(response);

                $scope.carregando = false;
                alert("Erro ao buscar a lista");
            });
    }

    $scope.buscarAlimentos = function (termo) {
        return $http.get('http://localhost:59266/listacompra/buscarAlimentos?termo=' + termo)
            .then(function (response) {
                return response.data;
            })
            .catch(function (response) {
                console.log(response);
                alert("Erro ao buscar alimento");
            });
    };

    $scope.total = function () {

        var total = $scope.alimentos.map(i => i.Preco * i.Quantidade).reduce((a, b) => {
            return a + b;
        })

        return total;
    }

    $scope.add = function () {
        if ($scope.alimentoBuscado) {

            //Aumentar a quantidade caso insira novamente um mesmo alimento
            //Implementar aqui...

            let item = JSON.parse(JSON.stringify($scope.alimentoBuscado));

            var ItemExistente = $scope.alimentos.filter(
                function (alimento) {
                    return alimento.Nome === item.Nome;
                }).map(i => i.Quantidade += 1)

            if (ItemExistente.length === 0) {
                $scope.alimentos.push(item);

            }
            console.log(ItemExistente)
            $scope.alimentoBuscado = "";
            $scope.total();


        }
    }

    $scope.remover = function (sAlimento, idx) {
        if (confirm("Tem certeza que deseja remover?")) {
            //Implementar aqui o codigo de remover...

            var listaNova = $scope.alimentos.filter(
                function (alimento) {
                    return alimento.Nome != sAlimento.Nome;
                })
            $scope.alimentos = listaNova;
        }
    }

    $scope.salvar = function () {
        $http.post('http://localhost:59266/listacompra/salvar', $scope.alimentos)
            .then(function (response) {
                console.log(response);
                alert("Lista salva com sucesso");

                popularLista();
            })
            .catch(function (response) {
                console.log(response);
                alert("Erro ao salvar a lista de compra");
            });
    }
});