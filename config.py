import os
from dotenv import load_dotenv

basedir = os.path.abspath(os.path.dirname(__file__))
load_dotenv(os.path.join(basedir, '.env'))


class Config():

    """Docstring for Config. """

    CONTROLLER_TEMPLATES=os.environ.get('CONTROLLER_TEMPLATES')
    VIEWS_TEMPLATES=os.environ.get('VIEWS_TEMPLATES')

