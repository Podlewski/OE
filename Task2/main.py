from matplotlib import pyplot as plt
from platypus import ZDT1, ZDT2, ZDT3
from platypus.algorithms import NSGAII, SPEA2, GDE3
from platypus.core import nondominated
from platypus.operators import UniformMutation
from pymoo.factory import get_problem

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
    algorithm = SPEA2(test_problem, args.population, variator=variator)
elif args.algorithm is 3:
    algorithm = GDE3(test_problem, args.population, variator=variator)

algorithm.run(args.generations)
res_x = [s.objectives[0] for s in algorithm.result]
res_y = [s.objectives[1] for s in algorithm.result]

pareto = get_problem(type(test_problem).__name__).pareto_front()
par_x = pareto[:, 0]
par_y = pareto[:, 1]

res_nod = nondominated(algorithm.result)
nod_x = [s.objectives[0] for s in res_nod]
nod_y = [s.objectives[1] for s in res_nod]

plt.scatter(res_x, res_y, color='red', label='Dominated', s=10)
plt.scatter(nod_x, nod_y, color='green', label='Nondominated', s=10)

plt.plot(par_x, par_y, color='blue', alpha=0.25, label='Pareto front')
plt.scatter(par_x, par_y, color='blue', s=10)

plt.grid(alpha=0.10)
plt.legend(loc='upper center', bbox_to_anchor=(0.5, 1.05),
           ncol=3, fancybox=True, shadow=True)

file_path = type(algorithm).__name__ + '_' + type(test_problem).__name__ + \
            '_g' + str(args.generations) + '_p' + str(args.population) + \
            '_r' + str(args.probability).replace('.', ',') 

plt.savefig(file_path, dpi=300)
