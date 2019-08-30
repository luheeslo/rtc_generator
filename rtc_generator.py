#!/home/lhel/Projetos/rtc_generator/.rtc_generator/bin/python

import os

from rtc_generator import gen_controller, gen_view, save
import click


@click.group()
def cli():
    "Automatiza tarefas para o projeto RTC"
    pass


@cli.command()
@click.argument('project_name')
@click.option('--model', '-m', help="Nome do modelo")
@click.option('--project', '-p', help="Caminho do projeto que contém o modelo")
def controller(project_name, model, project=None):
    """Criar controllers + views baseado no layout do projeto"""
    save(os.path.join(project_name, "Controllers", model + "Controller.cs"),
         gen_controller(project_name, model, project))
    
    view_model_path = os.path.join(project_name, "Views", model)
    os.mkdir(view_model_path)
    for name in ['Create', 'Delete', 'Edit', 'Details', 'Index']:
        save(os.path.join(view_model_path,  name + ".cshtml"),
             gen_view(project_name, name, model, project))


@cli.command()
@click.argument('project_name')
def view():
    click.echo('Create views')


if __name__ == "__main__":
    cli()
