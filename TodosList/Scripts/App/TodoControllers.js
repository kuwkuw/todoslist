var todoControllers = angular.module('todoControllers', []);

todoControllers.controller('todoCntrl', ['$scope', '$http', function($scope, $http) {

    $scope.isShow = {
        addCategoryForm: false,
        addTodoForm: false,
        addSubTodoForm: false
    };

    //Todos list
    $scope.todos = [];

    //
    $scope.newCategory = {};
    $scope.newTodo = {};
    $scope.newSubTodo = {};
   

    //Geting all todos from server
    $http.get('api/category').success(function (data) {
        console.log(data);
        $scope.todos = data;
    });

    //Showing/hiding addForm for adding category
    $scope.ShowForm = function(formeName) {

        if ($scope.isShow[formeName]) {
            $scope.isShow[formeName] = false;
        } else {
            $scope.isShow[formeName] = true;
        };
    };

    //Showing/hiding todos list in category
    $scope.ShowCategory = function(category) {
        $scope.todos.forEach(function(item) {
            item.isVisible = false;
        });
        category.isVisible = true;
    }

    //Showing/hiding;
    $scope.ShowAddSubTodoForm =function(todo) {
        $scope.todos.forEach(function(category) {
            category.Todos.forEach(function(todoItem) {
                todoItem.isVisibleAddSubTodoForm = false;
            });
        });
        todo.isVisibleAddSubTodoForm = true;
    }

    //Add new category to database
    $scope.AddNewCategory = function () {
        $http.post('api/category', $scope.newCategory).success(function (data) {
            $scope.todos.push(data);
            $scope.newCategory = {};
        });
    }

    //Delete category
    $scope.DelCategory = function(category) {
        $http.delete('api/category/' + category.CategoryId).success(function() {
            $scope.todos.splice($scope.todos.indexOf(category),1);
        });
    }

    //Add new todo to database
    $scope.AddNewTodo = function(catagoryId) {
        $scope.todos.forEach(function(element) {
            if (element.CategoryId === catagoryId && !$.isEmptyObject($scope.newTodo)) {
                var newTodo = formatToTodoObject(catagoryId);
                $http.post('api/todo', newTodo).success(function (data) {
                    element.Todos.push(data);
                    $scope.newTodo = {};
                });
            };
        });
    };

    //Delete todo
    $scope.DelTodo = function(todo) {
        $http.delete('api/todo/' + todo.TodoId).success(function() {
            $scope.todos.forEach(function(item) {
                if (todo.CategoryId === item.CategoryId) {
                    item.Todos.splice(item.Todos.indexOf(todo), 1);
                }
            });
        });
    }


    //Update todo status
    $scope.TodoIsChecked = function (todo) {
        $http.post('api/todo/' + todo.TodoId, todo).success(function(data) {
            if (todo.IsDone) {
                todo.SubTodos.forEach(function(subTodo) {
                    subTodo.IsDone = true;
                });
            } else {
                todo.SubTodos.forEach(function(subTodo) {
                    subTodo.IsDone = false;
                });
            }
        });
    }

    //Add new subtodo to database
    $scope.AddNewSubTodo = function (categoryId, todoId) {
        if (!$.isEmptyObject($scope.newSubTodo)) {
            $scope.todos.forEach(function (category) {
                if (category.CategoryId === categoryId) {
                    category.Todos.forEach(function (todo) {
                        if (todo.TodoId === todoId) {
                            $scope.newSubTodo.TodoId = todoId;
                            $http.post('api/subtodo', $scope.newSubTodo).success(function (data) {
                                todo.SubTodos.push(data);
                                $scope.newSubTodo = {};
                            });
                        }
                    });
                }
            });
        }
    };

    //Delete subtodo
    $scope.DelSubTodo = function (subTodo, categoryId) {
        console.log(subTodo, categoryId);
        $http.delete('api/subtodo/' + subTodo.SubTodoId).success(function() {
            $scope.todos.forEach(function(category) {
                if (category.CategoryId === categoryId) {
                    category.Todos.forEach(function(todo) {
                        if (todo.TodoId === subTodo.TodoId) {
                            todo.SubTodos.splice(todo.SubTodos.indexOf(subTodo), 1);
                        }
                    });
                }
            });
        });
    }

    //Update subtodo status
    $scope.SubTodoIsChecked = function(newSubTodo) {
        $http.post('api/subtodo/' + newSubTodo.SubTodoId, newSubTodo).success(function() {
            $scope.todos.forEach(function (category) {
                category.Todos.forEach(function(todo) {
                    if (todo.TodoId === newSubTodo.TodoId) {
                        var completed = 0;
                        todo.SubTodos.forEach(function (subTodo) {
                            if (subTodo.IsDone) {
                                completed++;
                            }
                        });
                        if (todo.SubTodos.length === completed) {
                            todo.IsDone = true;
                        }
                    }
                });
            });
        });
    }

    //Clear checked todos in category
    $scope.Clear = function (categoryId) {
        $http.post('api/category/' + categoryId).success(function(data) {
            $scope.todos.forEach(function(category) {
                if (category.CategoryId === data.CategoryId) {
                    category.Todos = data.Todos;
                }
            });
        });

    }

    //Formatting object in convenient type
    var formatToTodoObject = function(catagoryId) {
        return {
            CategoryId: catagoryId,
            DateTime: $scope.newTodo.DateTime,
            IsDone: false,
            SubTodos: [],
            Text: $scope.newTodo.Text
        };
    };

}]);