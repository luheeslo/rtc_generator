import os

class ParseCS():

    """Docstring for Parse. """

    def __init__(self, base_path_models):
        self.base_path_models = base_path_models


    def get_attrs(self, model):
        f = open(os.path.join(self.base_path_models, model + ".cs"))
        attrs = []
        for i in f.readlines():
            if "public" in i:
                line = [x for x in i.split(" ") if len(x) != 0]
                if line[1] != "class":
                    attrs.append(line[2])
        return attrs
