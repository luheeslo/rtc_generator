import os

from .core import render_code
from .parse import ParseCS
from .io import save
from config import Config

config = Config()

CONTROLLER_TEMPLATES = "Controllers/"
VIEWS_TEMPLATES = "Views/"


def gen_controller(mvc_project, model, project=None):
    """TODO: Docstring for gen_controller.

    :model: TODO
    :returns: TODO

    """
    models_path = os.path.join(project if project else mvc_project, "Models")
    attrs = ParseCS(models_path).get_attrs(model)
    args = {'model': model, 'attrs': attrs}
    return render_code(os.path.join(config.CONTROLLER_TEMPLATES, "TemplateController.cs"), **args)


def gen_view(mvc_project, name, model, project=None):
    """TODO: Docstring for gen_create_view.

    :model: TODO
    :returns: TODO

    """
    models_path = os.path.join(project if project else mvc_project, "Models")
    attrs = ParseCS(models_path).get_attrs(model)
    args = {'model': model, 'attrs': attrs}
    return render_code(os.path.join(config.VIEWS_TEMPLATES, "{0}.cshtml".format(name)), **args)
