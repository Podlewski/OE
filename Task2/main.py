from matplotlib import pyplot as plt
import numpy as np
from platypus import ZDT1, ZDT2, ZDT3
from platypus.algorithms import NSGAII, SPEA2, PESA2
from platypus.core import nondominated
from platypus.operators import UniformMutation
from pymoo.factory import get_problem, get_performance_indicator

from argument_parser import ArgumentParser

args = ArgumentParser().get_arguments()

if args.test_problem is 1:
    test_problem = ZDT1()
elif args.test_problem is 2:
    test_problem = ZDT2()
elif args.test_problem is 3:
    test_problem = ZDT3()

variator = UniformMutation(probability=args.probability,
                           perturbation=0.5)

if args.algorithm is 1:
    algorithm = NSGAII(test_problem, args.population, variator=variator)
elif args.algorithm is 2:
    if args.extra is None:
        algorithm = SPEA2(test_problem, args.population, variator=variator)
    else:    
        algorithm = SPEA2(test_problem, args.population, variator=variator, k=args.extra)
elif args.algorithm is 3:
    if args.extra is None:
        algorithm = PESA2(test_problem, args.population, variator=variator)
    else:    
        algorithm = PESA2(test_problem, args.population, variator=variator, divisions=args.extra)

algorithm.run(args.generations)
res_x = [s.objectives[0] for s in algorithm.result]
res_y = [s.objectives[1] for s in algorithm.result]

res_nod = nondominated(algorithm.result)
nod_x = [s.objectives[0] for s in res_nod]
nod_y = [s.objectives[1] for s in res_nod]
nod = np.array(list(zip(nod_x, nod_y)))

pareto = get_problem(type(test_problem).__name__).pareto_front()
par_x = pareto[:, 0]
par_y = pareto[:, 1]

plt.plot(par_x, par_y, alpha=0.25, color='blue', label='Pareto front')
plt.scatter(par_x, par_y, s=10, color='blue')

if args.algorithm is not 3: #PESA2
    plt.scatter(res_x, res_y, s=10, color='lime', label='Dominated')
plt.scatter(nod_x, nod_y, s=10, color='darkgreen', label='Nondominated')

plt.grid(alpha=0.10)
plt.legend(loc='upper center', bbox_to_anchor=(0.5, 1.05),
           ncol=3, fancybox=True, shadow=True)

file_path = str(type(algorithm).__name__).replace('2', 'II') + '_' + \
            type(test_problem).__name__ + '_g' + str(args.generations) + \
            '_p' + str(args.population) + \
            '_r' + str(args.probability).replace('.', ',')
if args.extra is not None:
    file_path += '_e' + str(args.extra)

plt.savefig(file_path + '.png', dpi=300)

gd = get_performance_indicator('gd', pareto)
gd_val = str(round(gd.calc(nod), 3)).replace('.', ',')

hv = get_performance_indicator('hv', ref_point=np.array([1, 5]))
hv_val = str(round(hv.calc(nod), 3)).replace('.', ',')

text_file = open(file_path + '.txt', 'wt', encoding='utf-8')
text_file.write(file_path.replace('_', ' ') + '\n\n')
text_file.write('GD'.ljust(10) + 'HV' + '\n')
text_file.write(gd_val.ljust(10) + hv_val)
text_file.close()

if args.print_on_screen is True:
    text_file = file_path.replace('_', ' ')
    text_file = text_file.replace('g', '')
    text_file = text_file.replace('p', '')
    text_file = text_file.replace('r', '')
    text_file = text_file.replace('e', '')
    args_str = text_file.split(' ')

    result_str = "".join(arg.ljust(10) for arg in args_str)

    result_str += gd_val.ljust(10) + hv_val

    print(result_str)
