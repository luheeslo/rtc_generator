from jinja2 import Environment, PackageLoader, select_autoescape

env = Environment(
    loader=PackageLoader('rtc_generator', 'templates'),
    autoescape=select_autoescape(['cshtml', 'cs'])
)

def render_code(template, **kwargs):
    temp = env.get_template(template)
    return temp.render(**kwargs)
